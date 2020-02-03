using Newtonsoft.Json;

namespace Grasews.API.Models
{
    /// <summary>
    ///
    /// </summary>
    public class SawsdlLiftingSchemaMapping_ApiRequestCreateModel
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("lifting_schema_mapping_url", NullValueHandling = NullValueHandling.Ignore)]
        public string LiftingSchemaMappingUrl { get; set; }
    }
}