namespace Grasews.Domain.Entities
{
    using Grasews.Domain.Enums;
    using System;

    public class ResetPassword : BaseEntity<int>
    {
        #region Props

        public string Email { get; set; }
        public int IdUser { get; set; }
        public string NewPassword { get; set; }
        public Guid ResetPasswordSecurity { get; set; }
        public ResetPasswordStatusEnum ResetPasswordStatus { get; set; }

        #endregion Props

        #region Navigations

        public virtual User User { get; set; }

        #endregion Navigations
    }
}