using Newtonsoft.Json;

namespace Grasews.API.Models
{
    /// <summary>
    ///
    /// </summary>
    public class ServiceDescription_User_ApiRequestCreateModel
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id_service_description", NullValueHandling = NullValueHandling.Ignore)]
        public int IdServiceDescription { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id_share_user", NullValueHandling = NullValueHandling.Ignore)]
        public int IdSharedUser { get; set; }
    }
}