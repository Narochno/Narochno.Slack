using System.Net.Http;

namespace Narochno.Slack
{
    public sealed class SlackConfig
    {
        /// <summary>
        /// An HTTP client to use if pooling connections.
        /// </summary>
        public HttpClient HttpClient { get; set; } = new HttpClient();

        /// <summary>
        /// The URL for an incoming webhook for the client to use.
        /// </summary>
        public string WebHookUrl { get; set; }

        /// <summary>
        /// The token for non-webhook API methods to use.
        /// </summary>
        public string Token { get; set; }

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
