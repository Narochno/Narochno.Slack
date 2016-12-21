﻿using Narochno.Primitives;

namespace Narochno.Slack
{
    public class SlackConfig
    {
        /// <summary>
        /// The URL for an incoming webhook for the client to use.
        /// </summary>
        public string WebHookUrl { get; set; }

        /// <summary>
        /// A username to post using
        /// Can be overridden in individual messages
        /// </summary>
        public Optional<string> Username { get; set; }

        /// <summary>
        /// A channel to post to (example: #general)
        /// Can be overridden in individual messages
        /// </summary>
        public Optional<string> Channel { get; set; }

        /// <summary>
        /// The emoji to use when posting (example: :ghost:)
        /// Can be overridden in individual messages
        /// </summary>
        public Optional<string> Emoji { get; set; }
    }
}
