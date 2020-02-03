using Newtonsoft.Json;

namespace Grasews.API.Models
{
    /// <summary>
    ///
    /// </summary>
    public class SawsdlModelReference_ApiRequestRemoveModel
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id_ontology_terms", NullValueHandling = NullValueHandling.Ignore)]
        public string IdOntologyTerms { get; set; }
    }
}