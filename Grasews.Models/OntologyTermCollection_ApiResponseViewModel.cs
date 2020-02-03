using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Grasews.API.Models
{
    /// <summary>
    ///
    /// </summary>
    public class OntologyTermCollection_ApiResponseViewModel
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("ontology_terms", NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<OntologyTermViewModel> OntologyTermViewModels { get; set; }

        /// <summary>
        ///
        /// </summary>
        public class OntologyTermViewModel : BaseModel<int>
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
            [JsonProperty("term_name", NullValueHandling = NullValueHandling.Ignore)]
            public string TermName { get; set; }
        }
    }
}