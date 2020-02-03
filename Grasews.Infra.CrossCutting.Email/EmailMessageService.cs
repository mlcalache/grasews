using Grasews.Domain.Interfaces.Services;
using Grasews.Infra.CrossCutting.Helpers;
using System;
using System.Net.Mail;

namespace Grasews.Infra.CrossCutting.Email
{
    public class EmailMessageService : IEmailMessageService
    {
        #region IEmailMessageService public methods

        public MailMessage CreateResetPasswordMessageMail(MailAddress mailFrom, string emailTo, Guid resetPasswordSecurity)
        {
            var html = EmailResources.EmailResetPassword;
            var baseUrlForAcceptInvitation = ConfigurationManagerHelper.BaseUrlForAcceptInvitation;
            var urlResetPassword = $"{baseUrlForAcceptInvitation}/Account/ResetPassword?s={resetPasswordSecurity}";
            var messageBody = string.Format(html, urlResetPassword);

            var mail = new MailMessage
            {
                From = mailFrom,
                Subject = "Grasews | Reset password",
                Body = messageBody,
                IsBodyHtml = true
            };
            mail.To.Add(emailTo);

            return mail;
        }

        public MailMessage CreateShareInvitationMessageMail(MailAddress mailFrom, string emailTo, Guid invitationSecurity)
        {
            var html = EmailResources.EmailShareInvitation;
            var baseUrlForAcceptInvitation = ConfigurationManagerHelper.BaseUrlForAcceptInvitation;
            var urlAccept = $"{baseUrlForAcceptInvitation}/ShareInvitation/AcceptInvitation?s={invitationSecurity}";
            var urlReject = $"{baseUrlForAcceptInvitation}/ShareInvitation/RejectInvitation?s={invitationSecurity}";
            var messageBody = string.Format(html, urlAccept, urlReject);

            var mail = new MailMessage
            {
                From = mailFrom,
                Subject = "Grasews | Invitation",
                Body = messageBody,
                IsBodyHtml = true
            };
            mail.To.Add(emailTo);

            return mail;
        }

        #endregion IEmailMessageService public methods

        #region Dispose

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion Dispose
    }
}