using Newtonsoft.Json;

namespace Narochno.Slack.Entities.Requests
{
    public abstract class BaseRequest
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
