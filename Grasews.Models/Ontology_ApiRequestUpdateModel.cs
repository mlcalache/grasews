using Newtonsoft.Json;

namespace Grasews.API.Models
{
    /// <summary>
    ///
    /// </summary>
    public class Ontology_ApiRequestUpdateModel : BaseModel<int>
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("ontology_name", NullValueHandling = NullValueHandling.Ignore)]
        public string OntologyName { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("xml", NullValueHandling = NullValueHandling.Ignore)]
        public string Xml { get; set; }
    }
}