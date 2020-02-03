using Newtonsoft.Json;

namespace Grasews.API.Models
{
    /// <summary>
    ///
    /// </summary>
    public class TaskComment_ApiRequestCreateModel
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("comment", NullValueHandling = NullValueHandling.Ignore)]
        public string Comment { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id_task", NullValueHandling = NullValueHandling.Ignore)]
        public int IdTask { get; set; }
    }
}