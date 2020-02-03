using Newtonsoft.Json;

namespace Grasews.API.Models
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class BaseModel<TKey>
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore, Order = -100)]
        public TKey Id { get; set; }
    }
}