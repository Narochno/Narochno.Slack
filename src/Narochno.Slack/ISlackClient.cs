using System.Threading.Tasks;
using Narochno.Slack.Entities;
using System;
using System.Threading;

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
    }
}