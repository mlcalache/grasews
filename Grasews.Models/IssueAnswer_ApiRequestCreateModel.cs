using Newtonsoft.Json;

namespace Grasews.API.Models
{
    /// <summary>
    ///
    /// </summary>
    public class IssueAnswer_ApiRequestCreateModel
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("answer", NullValueHandling = NullValueHandling.Ignore)]
        public string Answer { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id_issue", NullValueHandling = NullValueHandling.Ignore)]
        public int IdIssue { get; set; }
    }
}