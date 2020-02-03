using System;
using System.Net.Mail;

namespace Grasews.Domain.Interfaces.Services
{
    public interface IEmailMessageService : IBaseService
    {
        MailMessage CreateShareInvitationMessageMail(MailAddress mailFrom, string emailTo, Guid invitationSecurity);
        MailMessage CreateResetPasswordMessageMail(MailAddress mailFrom, string emailTo, Guid resetPasswordSecurity);
    }
}