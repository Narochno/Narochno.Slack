using Newtonsoft.Json;

namespace Narochno.Slack.Entities.Responses
{
    public abstract class BaseResponse
    {
        [JsonProperty("Ok")]
        public bool Ok { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
