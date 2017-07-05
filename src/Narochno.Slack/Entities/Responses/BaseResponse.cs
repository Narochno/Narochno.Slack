using Newtonsoft.Json;

namespace Narochno.Slack.Entities.Responses
{
    public abstract class BaseResponse
    {
        [JsonProperty("Ok")]
        public bool Ok { get; set; }
    }
}
