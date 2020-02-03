using Newtonsoft.Json;

namespace Grasews.Web.ViewModels
{
    public class Token_ApiResponseCreateModel
    {
        [JsonProperty(propertyName: "access_token")]
        public string access_token { get; set; }

        [JsonProperty(propertyName: "expires_in")]
        public int expires_in { get; set; }

        [JsonProperty(propertyName: "username")]
        public string Username { get; set; }

        [JsonProperty(propertyName: "userid")]
        public int UserId { get; set; }
    }
}