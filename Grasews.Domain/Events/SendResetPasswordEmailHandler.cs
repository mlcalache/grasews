using Grasews.Domain.Interfaces.Events;
using Grasews.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace Grasews.Domain.Events
{
    public class SendResetPasswordEmailHandler : IEventHandler<SendResetPasswordEmailEvent>
    {
        private readonly IEmailSenderService _emailService;

        public SendResetPasswordEmailHandler(IEmailSenderService emailService)
        {
            _emailService = emailService;
        }

        public void Handle(SendResetPasswordEmailEvent sendInvitationEmailEvent)
        {
            Task.Run(() => _emailService.SendResetPasswordEmail(sendInvitationEmailEvent.Email, sendInvitationEmailEvent.ResetPasswordSecurity));
        }
    }
}