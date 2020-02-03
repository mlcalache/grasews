using System;

namespace Grasews.API.Models
{
    public class ResetPassword_ApiRequestCreateModel
    {
        public int IdUser { get; set; }
        public string Password { get; set; }
        public Guid ResetPasswordSecurity { get; set; }
    }
}