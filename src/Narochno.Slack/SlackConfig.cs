

namespace Narochno.Slack
{
    public sealed class SlackConfig
    {
        /// <summary>
        /// The URL for an incoming webhook for the client to use.
        /// </summary>
        public string WebHookUrl { get; set; }

        /// <summary>
        /// The token for non-webhook API methods to use.
        /// </summary>
        public string Token { get; set; }
    }
}
