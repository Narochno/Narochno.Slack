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

        public async Task<SlackCode> PostMessage(string webHookUrl, Message message)
        {
            var response = await httpClient.PostAsync(webHookUrl, new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json"));

            var responseBody = await response.Content.ReadAsStringAsync();

            return (SlackCode)Enum.Parse(typeof(SlackCode), responseBody.Replace("_", string.Empty), true);
        }

        public void Dispose() => httpClient.Dispose();
    }
}