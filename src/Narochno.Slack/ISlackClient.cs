using System.Threading.Tasks;
using Narochno.Slack.Entities;
using System;

namespace Narochno.Slack
{
    public interface ISlackClient : IDisposable
    {
        /// <summary>
        /// Posts a message to Slack
        /// </summary>
        /// <param name="message">A message object</param>
        /// <returns>The status code from Slack</returns>
        Task<SlackCode> PostMessage(Message message);
    }
}