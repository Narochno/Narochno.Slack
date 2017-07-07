using Newtonsoft.Json;

namespace Narochno.Slack.Entities.Requests
{
    public class FilesDeleteRequest : BaseRequest
    {
        [JsonProperty("file", Required = Required.Always)]
        public string FileId { get; set; }
    }
}
