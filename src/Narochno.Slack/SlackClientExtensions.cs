using Narochno.Slack.Entities;
using System.Threading.Tasks;

namespace Narochno.Slack
{
    public static class SlackClientExtensions
    {
        /// <summary>
        /// Posts text content, without formatting.
        /// </summary>
        public static Task<SlackCode> PostText(this SlackClient slackClient, string text)
        {
            return slackClient.PostMessage(new Message { Text = text, Markdown = false });
        }

        /// <summary>
        /// Posts markdown-formatted text.
        /// </summary>
        public static Task<SlackCode> PostMarkdown(this SlackClient slackClient, string markdown)
        {
            return slackClient.PostMessage(new Message { Text = markdown, Markdown = true });
        }
    }
}
