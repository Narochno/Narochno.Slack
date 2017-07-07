using Narochno.Slack.Entities;
using Narochno.Slack.Entities.Requests;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Narochno.Slack
{
    public static class IncomingWebHookExtensions
    {
        /// <summary>
        /// Posts text content, without formatting.
        /// </summary>
        public static Task PostText(this ISlackClient slackClient, string text, CancellationToken ctx = default(CancellationToken))
        {
            return slackClient.IncomingWebHook(new IncomingWebHookRequest { Text = text, Markdown = false }, ctx);
        }

        /// <summary>
        /// Posts markdown-formatted text.
        /// </summary>
        public static Task PostMarkdown(this ISlackClient slackClient, string markdown, CancellationToken ctx = default(CancellationToken))
        {
            return slackClient.IncomingWebHook(new IncomingWebHookRequest { Text = markdown, Markdown = true }, ctx);
        }

        /// <summary>
        /// Posts attachments.
        /// </summary>
        public static Task PostAttachments(this ISlackClient slackClient, IEnumerable<Attachment> attachments, CancellationToken ctx = default(CancellationToken))
        {
            return slackClient.IncomingWebHook(new IncomingWebHookRequest { Attachments = attachments }, ctx);
        }
    }
}
