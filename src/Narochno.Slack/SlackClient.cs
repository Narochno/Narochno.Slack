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
using Narochno.Slack.Entities.Responses;
using Narochno.Slack.Entities.Requests;
using System.Collections.Generic;

namespace Narochno.Slack
{
    public class SlackClient : ISlackClient
    {
        private readonly HttpClient httpClient;
        private readonly SlackConfig slackConfig;
        private readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            Converters = new[] { new OptionalJsonConverter() }
        };

        /// <summary>
        /// Initialises a new Slack client with the supplied config object
        /// </summary>
        /// <param name="slackConfig">A confguration object</param>
        public SlackClient(HttpClient httpClient, SlackConfig slackConfig)
        {
            this.slackConfig = slackConfig ?? throw new ArgumentNullException(nameof(slackConfig));
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
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

            string json = JsonConvert.SerializeObject(message, serializerSettings);

            HttpResponseMessage response = await GetRetryPolicy().ExecuteAsync(() => httpClient.PostAsync(slackConfig.WebHookUrl, new StringContent(json, Encoding.UTF8, "application/json"), ctx));

            if (!response.IsSuccessStatusCode)
            {
                throw new SlackClientException(response.StatusCode, await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<ChannelsHistoryResponse> ChannelsHistory(ChannelsHistoryRequest request, CancellationToken token)
        {
            HttpResponseMessage response = await GetRetryPolicy().ExecuteAsync(() => httpClient.PostAsync("https://slack.com/api/channels.history", FormContentFromRequest(request), token));
            return await EnsureResponseSuccessful<ChannelsHistoryResponse>(response);
        }

        public async Task<ChatDeleteResponse> ChatDelete(ChatDeleteRequest request, CancellationToken token)
        {
            HttpResponseMessage response = await GetRetryPolicy().ExecuteAsync(() => httpClient.PostAsync("https://slack.com/api/chat.delete", FormContentFromRequest(request), token));
            return await EnsureResponseSuccessful<ChatDeleteResponse>(response);
        }

        public async Task<FilesListResponse> FilesList(FilesListRequest request, CancellationToken token)
        {
            HttpResponseMessage response = await GetRetryPolicy().ExecuteAsync(() => httpClient.PostAsync("https://slack.com/api/files.list", FormContentFromRequest(request), token));
            return await EnsureResponseSuccessful<FilesListResponse>(response);
        }

        public async Task<FilesDeleteResponse> FilesDelete(FilesDeleteRequest request, CancellationToken token)
        {
            HttpResponseMessage response = await GetRetryPolicy().ExecuteAsync(() => httpClient.PostAsync("https://slack.com/api/files.delete", FormContentFromRequest(request), token));
            return await EnsureResponseSuccessful<FilesDeleteResponse>(response);
        }

        public async Task<TResponse> EnsureResponseSuccessful<TResponse>(HttpResponseMessage response)
            where TResponse : BaseResponse
        {
            string raw = await response.Content.ReadAsStringAsync();

            TResponse deserialised;
            try
            {
                deserialised = JsonConvert.DeserializeObject<TResponse>(raw);
            }
            catch (JsonException)
            {
                throw new SlackClientException(response.StatusCode, raw);
            }

            if (!deserialised.Ok)
            {
                throw new SlackClientException(response.StatusCode, deserialised.Error);
            }

            return deserialised;
        }

        public FormUrlEncodedContent FormContentFromRequest(BaseRequest request)
        {
            try
            {
                string json = JsonConvert.SerializeObject(request, serializerSettings);
                IDictionary<string, string> dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json, serializerSettings);
                return new FormUrlEncodedContent(dictionary);
            }
            catch (JsonException ex)
            {
                throw new SlackClientException(HttpStatusCode.BadRequest, ex.Message);
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