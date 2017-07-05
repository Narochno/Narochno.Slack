using Newtonsoft.Json;

namespace Narochno.Slack.Entities.Requests
{
    public abstract class BaseRequest
    {
        [JsonProperty("token", Required = Required.Always)]
        public string Token { get; set; }
    }
}
