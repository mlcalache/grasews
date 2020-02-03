using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Grasews.API.Models
{
    /// <summary>
    ///
    /// </summary>
    public class OntologyCollection_ApiResponseViewModel
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("ontologies", NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<OntologyViewModel> OntologyViewModels { get; set; }

        /// <summary>
        ///
        /// </summary>
        public class OntologyViewModel : BaseModel<int>
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
            [JsonProperty("ontology_name", NullValueHandling = NullValueHandling.Ignore)]
            public string OntologyName { get; set; }
        }
    }
}