using Newtonsoft.Json;

namespace Grasews.API.Models
{
    /// <summary>
    ///
    /// </summary>
    public class Issue_ApiRequestCreateModel
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id_service_description", NullValueHandling = NullValueHandling.Ignore)]
        public int IdServiceDescription { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id_wsdl_interface", NullValueHandling = NullValueHandling.Ignore, Order = 5)]
        public int? IdWsdlInterface { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id_wsdl_operation", NullValueHandling = NullValueHandling.Ignore, Order = 5)]
        public int? IdWsdlOperation { get; set; }

        ///// <summary>
        /////
        ///// </summary>
        //[JsonProperty("id_wsdl_input", NullValueHandling = NullValueHandling.Ignore, Order = 5)]
        //public int? IdWsdlInput { get; set; }

        ///// <summary>
        /////
        ///// </summary>
        //[JsonProperty("id_wsdl_output", NullValueHandling = NullValueHandling.Ignore, Order = 5)]
        //public int? IdWsdlOutput { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id_wsdl_fault", NullValueHandling = NullValueHandling.Ignore, Order = 5)]
        public int? IdWsdlFault { get; set; }

        ///// <summary>
        /////
        ///// </summary>
        //[JsonProperty("id_wsdl_outfault", NullValueHandling = NullValueHandling.Ignore, Order = 5)]
        //public int? IdWsdlOutfault { get; set; }

        ///// <summary>
        /////
        ///// </summary>
        //[JsonProperty("id_wsdl_infault", NullValueHandling = NullValueHandling.Ignore, Order = 5)]
        //public int? IdWsdlInfault { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id_xsd_complex_type", NullValueHandling = NullValueHandling.Ignore, Order = 5)]
        public int? IdXsdComplexType { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id_xsd_simple_type", NullValueHandling = NullValueHandling.Ignore, Order = 5)]
        public int? IdXsdSimpleType { get; set; }

    }
}