using Newtonsoft.Json;

namespace Grasews.API.Models
{
    /// <summary>
    ///
    /// </summary>
    public class SawsdlModelReference_ApiRequestCreateModel
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id_ontology_term", NullValueHandling = NullValueHandling.Ignore)]
        public int IdOntologyTerm { get; set; }
    }
}