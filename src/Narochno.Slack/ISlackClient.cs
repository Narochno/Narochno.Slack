using System.Threading.Tasks;
using Narochno.Slack.Entities;
using System;

namespace Narochno.Slack
{
    public interface ISlackClient : IDisposable
    {
        Task<SlackCode> PostMessage(string webHookUrl, Message message);
    }
}