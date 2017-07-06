using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Narochno.Slack.Entities.Responses
{
    public class FilesInfoResponse : BaseResponse
    {
        [JsonProperty("file")]
        public File File { get; set; }
        [JsonProperty("comments")]
        public IList<Comment> Comments { get; set; } = new List<Comment>();
        [JsonProperty("paging")]
        public Paging Paging { get; set; }
    }
}
