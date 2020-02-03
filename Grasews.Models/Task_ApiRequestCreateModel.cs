using Newtonsoft.Json;

namespace Grasews.API.Models
{
    /// <summary>
    ///
    /// </summary>
    public class Task_ApiRequestCreateModel
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("done", NullValueHandling = NullValueHandling.Ignore)]
        public bool Done { get; set; }

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
    }
}