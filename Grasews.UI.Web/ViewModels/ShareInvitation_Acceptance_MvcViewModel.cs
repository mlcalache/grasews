using System.ComponentModel.DataAnnotations;

namespace Grasews.Web.ViewModels
{
    public class ShareInvitation_Acceptance_MvcViewModel : BaseMvcModel<int>
    {
        #region Public props

        [Compare("Password", ErrorMessage = "The password and the confirmation password do not match.")]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(WebResource))]
        [Required]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Email", ResourceType = typeof(WebResource))]
        [Required]
        public string Email { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof(WebResource))]
        [Required]
        public string FirstName { get; set; }

        [Required]
        public int IdServiceDescription { get; set; }

        [Required]
        public int IdSharedInvitation { get; set; }

        [Required]
        public int IdUserInviter { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(WebResource))]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Password", ResourceType = typeof(WebResource))]
        [Required]
        public string Password { get; set; }

        [Display(Name = "Username", ResourceType = typeof(WebResource))]
        //[RegularExpression("^[a-zA-Z]+(?:[a-zA-Z0-9])*$", ErrorMessage = "The username can consist of alphanumeric characters, both lowercase and capitals. It should not start with a number.")]
        [Required]
        public string Username { get; set; }

        #endregion Public props
    }
}