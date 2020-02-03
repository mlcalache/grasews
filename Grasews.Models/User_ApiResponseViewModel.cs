using Newtonsoft.Json;
using System;

namespace Grasews.API.Models
{
    /// <summary>
    ///
    /// </summary>
    public class User_ApiResponseViewModel : BaseModel<int>
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "first_name", NullValueHandling = NullValueHandling.Ignore)]
        public string FirstName { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "full_name", NullValueHandling = NullValueHandling.Ignore)]
        public string FullName => $"{FirstName} {LastName}";

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "is_admin", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsAdmin { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "last_name", NullValueHandling = NullValueHandling.Ignore)]
        public string LastName { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "registration_datetime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime RegistrationDateTime { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "username", NullValueHandling = NullValueHandling.Ignore)]
        public string Username { get; set; }
    }
}