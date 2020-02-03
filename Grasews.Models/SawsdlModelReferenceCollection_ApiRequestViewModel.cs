using Newtonsoft.Json;
using System.Collections.Generic;

namespace Grasews.API.Models
{
    /// <summary>
    ///
    /// </summary>
    public class SawsdlModelReferenceCollection_ApiRequestViewModel
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id_service_description", NullValueHandling = NullValueHandling.Ignore)]
        public int IdServiceDescription { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id_wsdl_element", NullValueHandling = NullValueHandling.Ignore)]
        public int IdWsdlElement { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("wsdl_element_name", NullValueHandling = NullValueHandling.Ignore)]
        public string WsdlElementName { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("sawsdl_model_references", NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<ModelReferenceViewModel> SawsdlModelReferenceViewModels { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("wsdl_element_type", NullValueHandling = NullValueHandling.Ignore)]
        public int IdWsdlElementType { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("wsdl_element_type_name", NullValueHandling = NullValueHandling.Ignore)]
        public string WsdlElementTypeName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public class ModelReferenceViewModel : BaseModel<int>
        {
            /// <summary>
            ///
            /// </summary>
            [JsonProperty("id_ontology_term", NullValueHandling = NullValueHandling.Ignore)]
            public int IdOntologyTerm { get; set; }

            /// <summary>
            ///
            /// </summary>
            [JsonProperty("id_service_description", NullValueHandling = NullValueHandling.Ignore)]
            public int IdServiceDescription { get; set; }

            /// <summary>
            ///
            /// </summary>
            [JsonProperty("model_reference", NullValueHandling = NullValueHandling.Ignore)]
            public string ModelReference { get; set; }
        }
    }
}