using System;
using System.ComponentModel.DataAnnotations;

namespace Grasews.Web.ViewModels
{
    public class ResetPassword_MvcViewModel : BaseMvcModel<int>
    {
        #region Public props

        [Compare("NewPassword", ErrorMessage = "The new password and the confirmation password do not match.")]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(WebResource))]
        [Required]
        public string ConfirmPassword { get; set; }

        public int IdUser { get; set; }

        [Display(Name = "NewPassword", ResourceType = typeof(WebResource))]
        [Required]
        public string NewPassword { get; set; }

        public Guid ResetPasswordSecurity { get; set; }

        #endregion Public props
    }
}