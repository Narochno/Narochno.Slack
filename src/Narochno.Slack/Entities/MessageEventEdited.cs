using Newtonsoft.Json;

namespace Narochno.Slack.Entities
{
    public class MessageEventEdited
    {
        [JsonProperty("user")]
        public string User { get; set; }
        [JsonProperty("ts")]
        public string Timestamp { get; set; }
    }
}
