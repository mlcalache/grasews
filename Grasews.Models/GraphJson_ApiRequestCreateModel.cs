using Newtonsoft.Json;

namespace Grasews.API.Models
{
    /// <summary>
    ///
    /// </summary>
    public class GraphJson_ApiRequestCreateModel
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("service_name", NullValueHandling = NullValueHandling.Ignore)]
        public string ServiceName { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("xml", NullValueHandling = NullValueHandling.Ignore)]
        public string Xml { get; set; }
    }
}