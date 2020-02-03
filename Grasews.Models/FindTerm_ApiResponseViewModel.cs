using Newtonsoft.Json;

namespace Grasews.API.Models
{
    /// <summary>
    ///
    /// </summary>
    public class FindTerm_ApiResponseViewModel : BaseModel<int>
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("term_name", NullValueHandling = NullValueHandling.Ignore)]
        public string TermName { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("term_uri", NullValueHandling = NullValueHandling.Ignore)]
        public string TermURI { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return TermName;
        }
    }
}