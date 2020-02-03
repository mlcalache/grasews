using Newtonsoft.Json;

namespace Grasews.API.Models
{
    /// <summary>
    ///
    /// </summary>
    public class SawsdlLoweringSchemaMapping_ApiRequestUpdateModel
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("lowering_schema_mapping_url", NullValueHandling = NullValueHandling.Ignore)]
        public string LoweringSchemaMappingUrl { get; set; }
    }
}