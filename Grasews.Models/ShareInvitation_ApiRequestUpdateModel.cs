using Grasews.Domain.Enums;
using Newtonsoft.Json;
using System;

namespace Grasews.API.Models
{
    /// <summary>
    ///
    /// </summary>
    public class ShareInvitation_ApiRequestUpdateModel : BaseModel<int>
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id_service_description", NullValueHandling = NullValueHandling.Ignore)]
        public int IdServiceDescription { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id_user_inviter", NullValueHandling = NullValueHandling.Ignore)]
        public int IdUserInviter { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("invitation_status", NullValueHandling = NullValueHandling.Ignore)]
        public InvitationStatusEnum InvitationStatus { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("invitation_security", NullValueHandling = NullValueHandling.Ignore)]
        public Guid InvitationSecurity { get; set; }
    }
}