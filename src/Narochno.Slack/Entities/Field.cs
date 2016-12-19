using Newtonsoft.Json;

namespace Narochno.Slack.Entities
{
    public class Field
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("@short")]
        public bool? Short { get; set; }
    }
}