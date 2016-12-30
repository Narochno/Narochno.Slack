﻿using Narochno.Primitives;
using Narochno.Primitives.Json;
using Narochno.Primitives.Parsing;
using Narochno.Slack.Entities;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        public async Task<SlackCode> PostMessage(Message message, CancellationToken ctx)
        {
            message.Username = message.Username.Fallback(slackConfig.Username);
            message.Channel = message.Channel.Fallback(slackConfig.Channel);
            message.Emoji = message.Emoji.Fallback(slackConfig.Emoji);

            var json = JsonConvert.SerializeObject(message, serializerSettings);

            var response = await httpClient.PostAsync(slackConfig.WebHookUrl, new StringContent(json, Encoding.UTF8, "application/json"), ctx);

            return (await response.Content.ReadAsStringAsync()).Parse<SlackCode>();
        }

        public void Dispose() => httpClient.Dispose();
    }
}