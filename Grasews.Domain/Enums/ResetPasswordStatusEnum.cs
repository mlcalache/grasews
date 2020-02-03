using System.ComponentModel;

namespace Grasews.Domain.Enums
{
    public enum ResetPasswordStatusEnum
    {
        [Description("The password reset has been requested")]
        Requested = 0,

        [Description("The password reset has been processed")]
        Processed = 1,
    }
}