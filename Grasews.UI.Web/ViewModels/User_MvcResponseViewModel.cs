using System.ComponentModel.DataAnnotations;

namespace Grasews.Web.ViewModels
{
    public class User_MvcCreateViewModel : BaseMvcModel<int>
    {
        //[RegularExpression("^[a-zA-Z]+(?:[a-zA-Z0-9])*$", ErrorMessage = "The username can consist of alphanumeric characters, both lowercase and capitals. It should not start with a number.")]
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [Required]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "FirstName", ResourceType = typeof(WebResource))]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "LastName", ResourceType = typeof(WebResource))]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email", ResourceType = typeof(WebResource))]
        public string Email { get; set; }
    }
}