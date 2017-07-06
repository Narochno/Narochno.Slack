using Narochno.Primitives;
using System;

namespace Narochno.Slack
{
    public class SlackConfig
    {
        /// <summary>
        /// The URL for an incoming webhook for the client to use.
        /// </summary>
        public string WebHookUrl { get; set; }

        /// <summary>
        /// The token for non-webhook API methods to use.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// A username to post using
        /// Can be overridden in individual messages
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// A channel to post to (example: #general)
        /// Can be overridden in individual messages
        /// </summary>
        public string Channel { get; set; }

        /// <summary>
        /// The emoji to use when posting (example: :ghost:)
        /// Can be overridden in individual messages
        /// </summary>
        public string Emoji { get; set; }

        /// <summary>
        /// The number of times requests will be retried 
        /// </summary>
        public int RetryAttempts { get; set; } = 2;

        /// <summary>
        /// The number of retries will be an exponent of this number
        /// </summary>
        public int RetryBackoffExponent { get; set; } = 2;
    }
}
