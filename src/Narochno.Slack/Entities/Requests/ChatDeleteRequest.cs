using Newtonsoft.Json;

namespace Narochno.Slack.Entities.Requests
{
    public class ChatDeleteRequest : BaseRequest
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }
        [JsonProperty("ts")]
        public string Timestamp { get; set; }
        [JsonProperty("as_user")]
        public bool AsUser { get; set; }
    }
}
