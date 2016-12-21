using Narochno.Primitives;
using Narochno.Primitives.Json;
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

        /// <summary>
        /// Posts a message to Slack
        /// </summary>
        /// <param name="message">A message object</param>
        /// <returns>The status code from Slack</returns>
        public async Task<SlackCode> PostMessage(Message message)
        {
            message.Username = message.Username.Fallback(slackConfig.Username);
            message.Channel = message.Channel.Fallback(slackConfig.Channel);
            message.Emoji = message.Emoji.Fallback(slackConfig.Emoji);

            var response = await httpClient.PostAsync(slackConfig.WebHookUrl, new StringContent(JsonConvert.SerializeObject(message, new OptionalJsonConverter()), Encoding.UTF8, "application/json"));

            var responseBody = await response.Content.ReadAsStringAsync();

            return (SlackCode)Enum.Parse(typeof(SlackCode), responseBody.Replace("_", string.Empty), true);
        }

        public void Dispose() => httpClient.Dispose();
    }
}