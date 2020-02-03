using Grasews.Domain.Enums;
using System;

namespace Grasews.API.Models
{
    public class ResetPassword_ApiResponseModel : BaseModel<int>
    {
        public int IdUser { get; set; }
        public string Email { get; set; }
        public Guid ResetPasswordSecurity { get; set; }
        public ResetPasswordStatusEnum ResetPasswordStatus { get; set; }
    }
}