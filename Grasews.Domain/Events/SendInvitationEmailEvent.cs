using Grasews.Domain.Interfaces.Events;
using System;

namespace Grasews.Domain.Events
{
    public class SendInvitationEmailEvent : IDomainEvent
    {
        public string Email { get; private set; }
        public Guid InvitationSecurity { get; private set; }

        public SendInvitationEmailEvent(string email, Guid invitationSecurity)
        {
            Email = email;
            InvitationSecurity = invitationSecurity;
        }
    }
}