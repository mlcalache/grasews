namespace Grasews.API.Models
{
    using Newtonsoft.Json;
    using System;

    public class TaskComment_ApiResponseViewModel : BaseModel<int>
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("comment", NullValueHandling = NullValueHandling.Ignore)]
        public string Comment { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id_owner_user", NullValueHandling = NullValueHandling.Ignore)]
        public int IdOwnerUser { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("owner_username", NullValueHandling = NullValueHandling.Ignore)]
        public string OwnerUsername { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("owner_user_email", NullValueHandling = NullValueHandling.Ignore)]
        public string OwnerUserEmail { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id_task", NullValueHandling = NullValueHandling.Ignore)]
        public int IdTask { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("registration_datetime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime RegistrationDateTime { get; set; }
    }
}