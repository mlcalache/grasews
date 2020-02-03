using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Grasews.Web.ViewModels
{
    /// <summary>
    ///
    /// </summary>
    public class Account_MvcViewModel : BaseMvcModel<int>
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "email")]
        [Required]
        [EmailAddress(ErrorMessage = "Invalid e-mail address")]
        public string Email { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
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
        [Required]
        [JsonProperty(PropertyName = "last_name", NullValueHandling = NullValueHandling.Ignore)]
        public string LastName { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "registration_datetime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? RegistrationDateTime { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        [JsonProperty(PropertyName = "username", NullValueHandling = NullValueHandling.Ignore)]
        public string Username { get; set; }
    }
}