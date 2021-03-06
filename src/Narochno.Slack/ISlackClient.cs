﻿using System.Threading.Tasks;
using System.Threading;
using Narochno.Slack.Entities.Requests;
using Narochno.Slack.Entities.Responses;

namespace Narochno.Slack
{
    public interface ISlackClient
    {
        Task IncomingWebHook(IncomingWebHookRequest request, CancellationToken ctx = default(CancellationToken));
        Task<ChannelsHistoryResponse> ChannelsHistory(ChannelsHistoryRequest request, CancellationToken token = default(CancellationToken));
        Task<EmojiListResponse> EmojiList(EmojiListRequest request, CancellationToken token = default(CancellationToken));
        Task<ChatDeleteResponse> ChatDelete(ChatDeleteRequest request, CancellationToken token = default(CancellationToken));
        Task<FilesListResponse> FilesList(FilesListRequest request, CancellationToken token = default(CancellationToken));
        Task<FilesInfoResponse> FilesInfo(FilesInfoRequest request, CancellationToken token = default(CancellationToken));
        Task<FilesDeleteResponse> FilesDelete(FilesDeleteRequest request, CancellationToken token = default(CancellationToken));
        Task<OAuthAccessResponse> OAuthAccess(OAuthAccessRequest request, CancellationToken token = default(CancellationToken));
    }
}