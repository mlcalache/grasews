using System.ComponentModel.DataAnnotations;

namespace Grasews.Web.ViewModels
{
    public class RequestResetPassword_MvcViewModel : BaseMvcModel<int>
    {
        #region Public props

        [Display(Name = "Email", ResourceType = typeof(WebResource))]
        [Required]
        [EmailAddress(ErrorMessage = "Invalid e-mail address")]
        public string Email { get; set; }

        #endregion
    }
}