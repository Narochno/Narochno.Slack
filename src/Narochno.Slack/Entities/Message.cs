using System.Collections.Generic;
using Newtonsoft.Json;

namespace Narochno.Slack.Entities
{
    public class Message
    {
        /// <summary>
        /// The mssage text
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// A channel to post to (example: #general)
        /// </summary>
        [JsonProperty("channel")]
        public string Channel { get; set; }

        /// <summary>
        /// A username to post using
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// The emoji to use when posting (example: :ghost:)
        /// </summary>
        [JsonProperty("icon_emoji")]
        public string Emoji { get; set; }

        /// <summary>
        /// Whether to parse the message text as markdown. Defaults to true.
        /// </summary>
        [JsonProperty("mrkdwn")]
        public bool Markdown { get; set; } = true;

        /// <summary>
        /// A list of attachments with this message
        /// </summary>
        [JsonProperty("attachments")]
        public IList<Attachment> Attachments { get; set; } = new List<Attachment>();
    }
}