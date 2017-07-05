using System.Threading.Tasks;
using Narochno.Slack.Entities;
using System;
using System.Threading;
using Narochno.Slack.Entities.Requests;
using Narochno.Slack.Entities.Responses;

namespace Narochno.Slack
{
    public interface ISlackClient : IDisposable
    {
        /// <summary>
        /// Posts a message to Slack
        /// </summary>
        /// <param name="message">A message object</param>
        /// <returns>The status code from Slack</returns>
        Task PostMessage(Message message, CancellationToken ctx = default(CancellationToken));
        Task<ChannelsHistoryResponse> ChannelsHistory(ChannelsHistoryRequest request, CancellationToken token = default(CancellationToken));
        Task<ChatDeleteResponse> ChatDelete(ChatDeleteRequest request, CancellationToken token = default(CancellationToken));
        Task<FilesListResponse> FilesList(FilesListRequest request, CancellationToken token = default(CancellationToken));
        Task<FilesDeleteResponse> FilesDelete(FilesDeleteRequest request, CancellationToken token = default(CancellationToken));
    }
}