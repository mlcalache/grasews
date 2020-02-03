//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Net.Mail;
//using System.Threading.Tasks;

//namespace Grasews.Infra.CrossCutting.Helpers
//{
//    public class MailHelper
//    {
//        #region Public methods

//        public static bool IsEmailValid(string emailaddress)
//        {
//            try
//            {
//                MailAddress m = new MailAddress(emailaddress);

//                return true;
//            }
//            catch (FormatException)
//            {
//                return false;
//            }
//        }

//        public static void Send(MailConfigHelper mailConfig, string destinatario, string assunto, string mensagem)
//        {
//            var mailMessageHelper = new MailMessageHelper
//            {
//                Mensagem = mensagem,
//                Assunto = assunto,
//                Destinatario = destinatario
//            };

//            Send(mailConfig, mailMessageHelper);
//        }

//        public static void Send(MailConfigHelper mailConfig, MailMessageHelper mailMessage, List<string> attachmentsPath = null)
//        {
//            using (var mail = GetMailMessage(mailConfig, mailMessage))
//            {
//                if (attachmentsPath != null && attachmentsPath.Any())
//                    attachmentsPath.ForEach(a => { if (File.Exists(a)) using (var attachment = new Attachment(a)) { mail.Attachments.Add(attachment); } });

//                var client = GetSmtpClient(mailConfig);

//                client.Send(mail);
//            }
//        }

//        public static async Task SendAsync(MailConfigHelper mailConfig, string destinatario, string assunto, string mensagem)
//        {
//            var mailMessageHelper = new MailMessageHelper
//            {
//                Mensagem = mensagem,
//                Assunto = assunto,
//                Destinatario = destinatario
//            };

//            await SendAsync(mailConfig, mailMessageHelper);
//        }

//        public static async Task SendAsync(MailConfigHelper mailConfig, MailMessageHelper mailMessage)
//        {
//            var mail = GetMailMessage(mailConfig, mailMessage);

//            var client = GetSmtpClient(mailConfig);

//            await client.SendMailAsync(mail);
//        }

//        #endregion Public methods

//        #region Private methods

//        private static MailMessage GetMailMessage(MailConfigHelper mailConfig, MailMessageHelper mailMessage)
//        {
//            var mail = new MailMessage
//            {
//                From = new MailAddress(mailConfig.NomeUsuario),
//                Subject = mailMessage.Assunto,
//                IsBodyHtml = true,
//                Body = mailMessage.Mensagem
//            };
//            mail.To.Add(new MailAddress(mailMessage.Destinatario));
//            return mail;
//        }

//        private static SmtpClient GetSmtpClient(MailConfigHelper mailConfig)
//        {
//            var client = new SmtpClient(mailConfig.Servidor, mailConfig.Porta)
//            {
//                EnableSsl = mailConfig.SSL,
//                DeliveryMethod = SmtpDeliveryMethod.Network,
//                Credentials = new NetworkCredential(mailConfig.NomeUsuario, mailConfig.Senha)
//            };
//            return client;
//        }

//        #endregion Private methods
//    }
//}