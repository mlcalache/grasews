using Newtonsoft.Json;
using System;

namespace Grasews.API.Models
{
    /// <summary>
    ///
    /// </summary>
    public class ServiceDescription_User_ApiResponseViewModel : BaseModel<int>
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id_owner_user", NullValueHandling = NullValueHandling.Ignore)]
        public int IdOwnerUser { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id_service_description", NullValueHandling = NullValueHandling.Ignore)]
        public int IdServiceDescription { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id_shared_user", NullValueHandling = NullValueHandling.Ignore)]
        public int IdSharedUser { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("registration_datetime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime RegistrationDateTime { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("service_name", NullValueHandling = NullValueHandling.Ignore)]
        public string ServiceName { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("shared_user_email", NullValueHandling = NullValueHandling.Ignore)]
        public string SharedUserEmail { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("share_user_first_name", NullValueHandling = NullValueHandling.Ignore)]
        public string SharedUserFirstName { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("shared_user_last_name", NullValueHandling = NullValueHandling.Ignore)]
        public string SharedUserLastName { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("shared_user_username", NullValueHandling = NullValueHandling.Ignore)]
        public string SharedUserUsername { get; set; }
    }
}