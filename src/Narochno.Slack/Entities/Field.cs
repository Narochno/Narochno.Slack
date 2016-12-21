using Narochno.Primitives;
using Newtonsoft.Json;

namespace Narochno.Slack.Entities
{
    public class Field
    {
        /// <summary>
        /// Shown as a bold heading above the value text. It cannot contain markup and will be escaped for you.
        /// </summary>
        [JsonProperty("title")]
        public Optional<string> Title { get; set; }

        /// <summary>
        /// The text value of the field. It may contain standard message markup and must be escaped as normal. May be multi-line.
        /// </summary>
        [JsonProperty("value")]
        public Optional<string> Value { get; set; }

        /// <summary>
        /// An optional flag indicating whether the value is short enough to be displayed side-by-side with other values.
        /// </summary>
        [JsonProperty("short")]
        public bool Short { get; set; }
    }
}