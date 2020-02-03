using System;

namespace Grasews.Domain.Interfaces.Services
{
    public interface IEmailSenderService : IBaseService
    {
        void SendInvitationEmail(string emailTo, Guid invitationSecurity);

        void SendResetPasswordEmail(string emailTo, Guid resetPasswordSecurity);
    }
}