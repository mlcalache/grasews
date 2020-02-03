using Newtonsoft.Json;
using System;

namespace Grasews.API.Models
{
    /// <summary>
    ///
    /// </summary>
    public class IssueAnswer_ApiResponseViewModel : BaseModel<int>
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("answer", NullValueHandling = NullValueHandling.Ignore)]
        public string Answer { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id_issue", NullValueHandling = NullValueHandling.Ignore)]
        public int IdIssue { get; set; }

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
        [JsonProperty("registration_datetime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime RegistrationDateTime { get; set; }
    }
}