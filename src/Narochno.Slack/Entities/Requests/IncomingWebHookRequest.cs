using Newtonsoft.Json;
using System.Collections.Generic;

namespace Narochno.Slack.Entities.Requests
{
    public class IncomingWebHookRequest
    {
        /// <summary>
        /// The mssage text
        /// </summary>
        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }

        /// <summary>
        /// A channel to post to (example: #general)
        /// </summary>
        [JsonProperty("channel", NullValueHandling = NullValueHandling.Ignore)]
        public string Channel { get; set; }

        /// <summary>
        /// A username to post using
        /// </summary>
        [JsonProperty("username", NullValueHandling = NullValueHandling.Ignore)]
        public string Username { get; set; }

        /// <summary>
        /// The emoji to use when posting (example: :ghost:)
        /// </summary>
        [JsonProperty("icon_emoji", NullValueHandling = NullValueHandling.Ignore)]
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
        public IEnumerable<Attachment> Attachments { get; set; } = new List<Attachment>();
    }
}
