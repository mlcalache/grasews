using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Grasews.API.Models
{
    /// <summary>
    ///
    /// </summary>
    public class ServiceDescriptionCollection_ApiResponseViewModel
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("service_descriptions", NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<ServiceDescriptionViewModel> ServiceDescriptionViewModels { get; set; }

        /// <summary>
        ///
        /// </summary>
        public class ServiceDescriptionViewModel : BaseModel<int>
        {
            /// <summary>
            ///
            /// </summary>
            [JsonProperty("id_owner_user", NullValueHandling = NullValueHandling.Ignore)]
            public int IdOwnerUser { get; set; }

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
        }
    }
}