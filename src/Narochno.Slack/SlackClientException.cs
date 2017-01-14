using System.Net;
using System.Net.Http;

namespace Narochno.Slack
{
    public class SlackClientException : HttpRequestException
    {
        public SlackClientException(HttpStatusCode statusCode, string error)
            : base($"Status: {statusCode}, Error: {error}")
        {
            StatusCode = statusCode;
            Error = error;
        }

        public HttpStatusCode StatusCode { get; }
        public string Error { get; }
    }
}
