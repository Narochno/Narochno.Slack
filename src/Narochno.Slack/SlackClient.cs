using Narochno.Primitives;
using Narochno.Primitives.Json;
using Narochno.Slack.Entities;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using Polly;
using Polly.Retry;

namespace Narochno.Slack
{
    public class SlackClient : ISlackClient
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly SlackConfig slackConfig;
        private readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            Converters = new[] { new OptionalJsonConverter() }
        };

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
        public async Task PostMessage(Message message, CancellationToken ctx)
        {
            message.Username = message.Username.Fallback(slackConfig.Username);
            message.Channel = message.Channel.Fallback(slackConfig.Channel);
            message.Emoji = message.Emoji.Fallback(slackConfig.Emoji);

            var json = JsonConvert.SerializeObject(message, serializerSettings);

            var response = await GetRetryPolicy().ExecuteAsync(() => httpClient.PostAsync(slackConfig.WebHookUrl, new StringContent(json, Encoding.UTF8, "application/json"), ctx));

            if (!response.IsSuccessStatusCode)
            {
                throw new SlackClientException(response.StatusCode, await response.Content.ReadAsStringAsync());
            }
        }

        public RetryPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return Policy
                .HandleResult<HttpResponseMessage>(r => r.StatusCode >= HttpStatusCode.InternalServerError)
                .WaitAndRetryAsync(slackConfig.RetryAttempts, retryAttempt => TimeSpan.FromSeconds(Math.Pow(slackConfig.RetryBackoffExponent, retryAttempt)));
        }

        public void Dispose() => httpClient.Dispose();
    }
}