using Newtonsoft.Json;
using System.Collections.Generic;

namespace Grasews.API.Models
{
    /// <summary>
    ///
    /// </summary>
    public class ShareInvitation_ApiRequestCreateModel
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("emails", NullValueHandling = NullValueHandling.Ignore)]
        public string Emails { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id_service_description", NullValueHandling = NullValueHandling.Ignore)]
        public int IdServiceDescription { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id_ontologies", NullValueHandling = NullValueHandling.Ignore)]
        public string IdOntologies { get; set; }
    }
}