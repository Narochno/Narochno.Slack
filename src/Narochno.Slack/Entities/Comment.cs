using Newtonsoft.Json;

namespace Narochno.Slack.Entities
{
    public class Comment
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
        [JsonProperty("user")]
        public string User { get; set; }
        [JsonProperty("comment")]
        public string CommentBody { get; set; }
    }
}
