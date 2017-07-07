using Newtonsoft.Json;
using System.Collections.Generic;

namespace Narochno.Slack.Entities.Responses
{
    public class EmojiListResponse : BaseResponse
    {
        [JsonProperty("emoji")]
        public IDictionary<string, string> Emoji { get; set; } = new Dictionary<string, string>();
    }
}
