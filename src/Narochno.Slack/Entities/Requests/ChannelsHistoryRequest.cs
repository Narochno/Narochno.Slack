using Newtonsoft.Json;

namespace Narochno.Slack.Entities.Requests
{
    public class ChannelsHistoryRequest : BaseRequest
    {
        [JsonProperty("channel", Required = Required.Always)]
        public string Channel { get; set; }
        [JsonProperty("count", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? Count { get; set; }
        [JsonProperty("inclusive", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? Inclusive { get; set; }
        [JsonProperty("latest", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Latest { get; set; }
        [JsonProperty("oldest", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Oldest { get; set; }
        [JsonProperty("unread", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? Unread { get; set; }
    }
}
