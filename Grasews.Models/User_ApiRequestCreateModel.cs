using Newtonsoft.Json;

namespace Grasews.API.Models
{
    /// <summary>
    ///
    /// </summary>
    public class User_ApiRequestCreateModel
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "is_admin")]
        public bool IsAdmin { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }
    }
}