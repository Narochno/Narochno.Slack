using Newtonsoft.Json;
using System.Collections.Generic;

namespace Narochno.Slack.Entities
{
    public class MessageEventReaction
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("users")]
        public IList<string> Users { get; set; } = new List<string>();
    }
}
