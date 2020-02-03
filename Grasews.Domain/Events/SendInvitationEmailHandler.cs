using Grasews.Domain.Interfaces.Events;
using Grasews.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace Grasews.Domain.Events
{
    public class SendInvitationEmailHandler : IEventHandler<SendInvitationEmailEvent>
    {
        private readonly IEmailSenderService _emailService;

        public SendInvitationEmailHandler(IEmailSenderService emailService)
        {
            _emailService = emailService;
        }

        public void Handle(SendInvitationEmailEvent sendInvitationEmailEvent)
        {
            Task.Run(() => _emailService.SendInvitationEmail(sendInvitationEmailEvent.Email, sendInvitationEmailEvent.InvitationSecurity));
        }
    }
}