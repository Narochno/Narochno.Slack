using Newtonsoft.Json;

namespace Narochno.Slack.Entities.Requests
{
    public class FilesListRequest : BaseRequest
    {
        [JsonProperty("channel", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Channel { get; set; }
        [JsonProperty("count", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? Count { get; set; }
        [JsonProperty("page", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? Page { get; set; }
        [JsonProperty("ts_from", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string TimestampFrom { get; set; }
        [JsonProperty("ts_to", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string TimestampTo { get; set; }
        [JsonProperty("types", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Types { get; set; } = "all";
        [JsonProperty("user", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string User { get; set; }
    }
}
