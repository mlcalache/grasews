using Grasews.Domain.Interfaces.Services;
using Grasews.Infra.CrossCutting.Helpers;
using System;
using System.Net;
using System.Net.Mail;

namespace Grasews.Infra.ExternalService.Gmail
{
    public class GmailService : IGmailService
    {
        #region Private vars

        private readonly IEmailMessageService _emailMessageService;

        #endregion Private vars

        #region Ctors

        public GmailService(IEmailMessageService emailMessageService)
        {
            _emailMessageService = emailMessageService;
        }

        #endregion Ctors

        #region Dispose

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion Dispose

        #region Private methods

        private SmtpClient CreateGmailSmtpClient()
        {
            var smtp = new SmtpClient("smtp.gmail.com")
            {
                EnableSsl = true,
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("grasews@gmail.com", CryptHelper.Decrypt(ConfigurationManagerHelper.EmailPassword))
            };

            return smtp;
        }

        #endregion Private methods

        #region Public methods

        public void SendInvitationEmail(string emailTo, Guid invitationSecurity)
        {
            var mailFrom = new MailAddress("grasews@gmail.com", "Grasews");

            var mail = _emailMessageService.CreateShareInvitationMessageMail(mailFrom, emailTo, invitationSecurity);

            using (var smtp = CreateGmailSmtpClient())
            {
                smtp.Send(mail);
            }
        }

        public void SendResetPasswordEmail(string emailTo, Guid resetPasswordSecurity)
        {
            var mailFrom = new MailAddress("grasews@gmail.com", "Grasews");

            var mail = _emailMessageService.CreateResetPasswordMessageMail(mailFrom, emailTo, resetPasswordSecurity);

            using (var smtp = CreateGmailSmtpClient())
            {
                smtp.Send(mail);
            }
        }

        #endregion Public methods
    }
}