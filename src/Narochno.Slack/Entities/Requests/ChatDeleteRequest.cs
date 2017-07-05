using Newtonsoft.Json;

namespace Narochno.Slack.Entities.Requests
{
    public class ChatDeleteRequest : BaseRequest
    {
        [JsonProperty("channel", Required = Required.Always)]
        public string Channel { get; set; }
        [JsonProperty("ts", Required = Required.Always)]
        public string Timestamp { get; set; }
        [JsonProperty("as_user", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? AsUser { get; set; }
    }
}
