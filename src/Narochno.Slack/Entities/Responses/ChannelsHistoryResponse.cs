using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Narochno.Slack.Entities.Responses
{
    public class ChannelsHistoryResponse : BaseResponse
    {
        [JsonProperty("latest")]
        public string Latest { get; set; }
        [JsonProperty("has_more")]
        public bool HasMore { get; set; }
        [JsonProperty("messages")]
        public IList<MessageEvent> Messages { get; set; } = new List<MessageEvent>();
    }
}
