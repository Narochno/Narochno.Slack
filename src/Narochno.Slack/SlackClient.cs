using Narochno.Primitives.Json;
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

        public SlackClient(HttpClient httpClient, SlackConfig slackConfig)
        {
            this.slackConfig = slackConfig ?? throw new ArgumentNullException(nameof(slackConfig));
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task IncomingWebHook(IncomingWebHookRequest request, CancellationToken ctx)
        {
            string url = slackConfig.WebHookUrl ?? throw new SlackClientException(HttpStatusCode.BadRequest, $"{nameof(slackConfig.WebHookUrl)} must have a value");
            string json = JsonConvert.SerializeObject(request, serializerSettings);

            HttpResponseMessage response = await GetRetryPolicy().ExecuteAsync(() => httpClient.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"), ctx));

            string raw = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode || raw != "ok")
            {
                throw new SlackClientException(response.StatusCode, raw);
            }
        }

        public Task<EmojiListResponse> EmojiList(EmojiListRequest request, CancellationToken token) => MakeRequest<EmojiListRequest, EmojiListResponse>(request, "emoji.list", token);
        public Task<ChannelsHistoryResponse> ChannelsHistory(ChannelsHistoryRequest request, CancellationToken token) => MakeRequest<ChannelsHistoryRequest, ChannelsHistoryResponse>(request, "channels.history", token);
        public Task<ChatDeleteResponse> ChatDelete(ChatDeleteRequest request, CancellationToken token) => MakeRequest<ChatDeleteRequest, ChatDeleteResponse>(request, "chat.delete", token);
        public Task<FilesListResponse> FilesList(FilesListRequest request, CancellationToken token) => MakeRequest<FilesListRequest, FilesListResponse>(request, "files.list", token);
        public Task<FilesInfoResponse> FilesInfo(FilesInfoRequest request, CancellationToken token) => MakeRequest<FilesInfoRequest, FilesInfoResponse>(request, "files.info", token);
        public Task<FilesDeleteResponse> FilesDelete(FilesDeleteRequest request, CancellationToken token) => MakeRequest<FilesDeleteRequest, FilesDeleteResponse>(request, "files.delete", token);
        public Task<OAuthAccessResponse> OAuthAccess(OAuthAccessRequest request, CancellationToken token) => MakeRequest<OAuthAccessRequest, OAuthAccessResponse>(request, "oauth.access", token);

        public async Task<TResponse> MakeRequest<TRequest, TResponse>(TRequest request, string method, CancellationToken token)
            where TRequest : BaseRequest
            where TResponse : BaseResponse
        {
            request.Token = request.Token ?? slackConfig.Token ?? throw new SlackClientException(HttpStatusCode.BadRequest, $"{nameof(slackConfig.Token)} is required to make this request");

            HttpResponseMessage response = await GetRetryPolicy().ExecuteAsync(() => httpClient.PostAsync($"https://slack.com/api/{method}", FormContentFromRequest(request), token));
            return await EnsureResponseSuccessful<TResponse>(response);
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