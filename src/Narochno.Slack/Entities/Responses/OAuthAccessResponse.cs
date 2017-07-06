using Newtonsoft.Json;

namespace Narochno.Slack.Entities.Responses
{
    public class OAuthAccessResponse : BaseResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("scope")]
        public string Scope { get; set; }
    }
}
