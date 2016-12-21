using Narochno.Slack.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Narochno.Slack
{
    public static class SlackClientExtensions
    {
        /// <summary>
        /// Posts text content, without formatting.
        /// </summary>
        public static Task<SlackCode> PostText(this ISlackClient slackClient, string text)
        {
            return slackClient.PostMessage(new Message { Text = text, Markdown = false });
        }

        /// <summary>
        /// Posts markdown-formatted text.
        /// </summary>
        public static Task<SlackCode> PostMarkdown(this ISlackClient slackClient, string markdown)
        {
            return slackClient.PostMessage(new Message { Text = markdown, Markdown = true });
        }

        /// <summary>
        /// Posts attachments.
        /// </summary>
        public static Task<SlackCode> PostAttachments(this ISlackClient slackClient, IEnumerable<Attachment> attachments)
        {
            return slackClient.PostMessage(new Message { Attachments = attachments });
        }
    }
}
