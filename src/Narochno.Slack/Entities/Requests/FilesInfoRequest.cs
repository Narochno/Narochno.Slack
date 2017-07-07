using Newtonsoft.Json;

namespace Narochno.Slack.Entities.Requests
{
    public class FilesInfoRequest : BaseRequest
    {
        [JsonProperty("file", Required = Required.Always)]
        public string FileId { get; set; }
        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public int Count { get; set; }
        [JsonProperty("page", NullValueHandling = NullValueHandling.Ignore)]
        public int Page { get; set; }
    }
}
