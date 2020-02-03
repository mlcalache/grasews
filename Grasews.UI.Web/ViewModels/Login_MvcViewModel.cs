using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Grasews.Web.ViewModels
{
    public class Login_MvcViewModel : BaseMvcModel<int>
    {
        [DisplayName("Username")]
        //[RegularExpression("^[a-zA-Z]+(?:[a-zA-Z0-9])*$", ErrorMessage = "The username can consist of alphanumeric characters, both lowercase and capitals. It should not start with a number.")]
        [Required]
        [JsonProperty("username", NullValueHandling = NullValueHandling.Ignore)]
        public string Username { get; set; }

        [DisplayName("Password")]
        [Required]
        [JsonProperty("password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

        [DisplayName("Remember me")]
        [JsonProperty("remember", NullValueHandling = NullValueHandling.Ignore)]
        public bool Remember { get; set; }
    }
}