using Newtonsoft.Json;

namespace Grasews.API.Models
{
    /// <summary>
    ///
    /// </summary>
    public class SawsdlLoweringSchemaMapping_ApiRequestViewModel
    { /// <summary>
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
        [JsonProperty("wsdl_element_type", NullValueHandling = NullValueHandling.Ignore)]
        public int IdWsdlElementType { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string LoweringSchemaMappingUrl { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("wsdl_element_name", NullValueHandling = NullValueHandling.Ignore)]
        public string WsdlElementName { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("wsdl_element_type_name", NullValueHandling = NullValueHandling.Ignore)]
        public string WsdlElementTypeName { get; set; }
    }
}