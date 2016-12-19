using System.Collections.Generic;
using Newtonsoft.Json;

namespace Narochno.Slack.Entities
{
    public class Attachment
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("fallback")]
        public string Fallback { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("fields")]
        public IList<Field> Fields { get; set; }

        [JsonProperty("mrkdwn_in")]
        public IList<string> MrkdwnIn { get; set; }
    }
}