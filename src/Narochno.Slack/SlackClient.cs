using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using Narochno.Slack.Entities.Responses;
using Narochno.Slack.Entities.Requests;
using System.Collections.Generic;

namespace Narochno.Slack
{
    public sealed class SlackClient : ISlackClient
    {
        private readonly HttpClient _httpClient; 
        private readonly SlackConfig _config;

        public SlackClient(SlackConfig config, HttpClient httpClient)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _httpClient = httpClient;
        }

        public async Task IncomingWebHook(IncomingWebHookRequest request, CancellationToken ctx)
        {
            string url = _config.WebHookUrl ?? throw new SlackClientException(HttpStatusCode.BadRequest, $"{nameof(_config.WebHookUrl)} must have a value");
            string json = JsonConvert.SerializeObject(request);

            HttpResponseMessage response = await _httpClient.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"), ctx);

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
            request.Token = request.Token ?? _config.Token ?? throw new SlackClientException(HttpStatusCode.BadRequest, $"{nameof(_config.Token)} is required to make this request");

            HttpResponseMessage response = await _httpClient.PostAsync($"https://slack.com/api/{method}", FormContentFromRequest(request), token);
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
                string json = JsonConvert.SerializeObject(request);
                IDictionary<string, string> dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                return new FormUrlEncodedContent(dictionary);
            }
            catch (JsonException ex)
            {
                throw new SlackClientException(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}