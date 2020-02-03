using Newtonsoft.Json;

namespace Grasews.API.Models
{
    /// <summary>
    ///
    /// </summary>
    public class UpdateGraphJson_ApiRequestUpdateModel
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("graph_json", NullValueHandling = NullValueHandling.Ignore)]
        public string GraphJson { get; set; }
    }
}