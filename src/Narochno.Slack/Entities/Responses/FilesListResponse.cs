using Newtonsoft.Json;
using System.Collections.Generic;

namespace Narochno.Slack.Entities.Responses
{

    public class FilesListResponse : BaseResponse
    {
        [JsonProperty("files")]
        public IList<File> Files { get; set; } = new List<File>();
        [JsonProperty("paging")]
        public Paging Paging { get; set; }
    }
}
