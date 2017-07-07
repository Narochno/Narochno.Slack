using Newtonsoft.Json;

namespace Narochno.Slack.Entities.Responses
{
    public class ChatDeleteResponse : BaseResponse
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }
        [JsonProperty("ts")]
        public string Timestamp { get; set; }
        [JsonProperty("as_user")]
        public bool AsUser { get; set; }
    }
}
