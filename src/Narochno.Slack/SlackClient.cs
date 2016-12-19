using Narochno.Slack.Entities;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Narochno.Slack
{
    public class SlackClient : ISlackClient
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly SlackConfig slackConfig;

        /// <summary>
        /// Initialises a new Slack client with the supplied config object
        /// </summary>
        /// <param name="slackConfig">A confguration object</param>
        public SlackClient(SlackConfig slackConfig)
        {
            if (slackConfig == null)
            {
                throw new ArgumentNullException(nameof(slackConfig));
            }

            this.slackConfig = slackConfig;
        }

        public async Task<SlackCode> PostMessage(Message message)
        {
            var response = await httpClient.PostAsync(slackConfig.WebHookUrl, new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json"));

            var responseBody = await response.Content.ReadAsStringAsync();

            return (SlackCode)Enum.Parse(typeof(SlackCode), responseBody.Replace("_", string.Empty), true);
        }

        public void Dispose() => httpClient.Dispose();
    }
}