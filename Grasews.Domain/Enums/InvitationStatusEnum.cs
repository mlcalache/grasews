using System.ComponentModel;

namespace Grasews.Domain.Enums
{
    public enum InvitationStatusEnum
    {
        [Description("The invitation has been sent")]
        Invited = 0,

        [Description("The invitation has been accepted")]
        Accepted = 1,

        [Description("The invitation has been rejected")]
        Rejected = 2,

        [Description("The invitation has been revoked")]
        Revoked = 3
    }
}