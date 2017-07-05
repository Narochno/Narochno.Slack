using Newtonsoft.Json;

namespace Narochno.Slack.Entities.Requests
{
    public class ChannelsHistoryRequest : BaseRequest
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; } = 100;
        [JsonProperty("inclusive")]
        public bool Inclusive { get; set; }
        [JsonProperty("latest")]
        public string Latest { get; set; }
        [JsonProperty("oldest")]
        public string Oldest { get; set; }
        [JsonProperty("unread")]
        public bool Unread { get; set; }
    }
}
