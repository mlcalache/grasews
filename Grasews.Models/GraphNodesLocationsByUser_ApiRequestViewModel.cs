using Newtonsoft.Json;

namespace Grasews.API.Models
{
    /// <summary>
    ///
    /// </summary>
    public class GraphNodesLocationsByUser_ApiRequestViewModel
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id_service_description", NullValueHandling = NullValueHandling.Ignore)]
        public int IdServiceDescription { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("original_graph_json", NullValueHandling = NullValueHandling.Ignore)]
        public string OriginalGraphJson { get; set; }
    }
}