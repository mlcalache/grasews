using Grasews.Domain.Interfaces.Events;
using System;

namespace Grasews.Domain.Events
{
    public class SendResetPasswordEmailEvent : IDomainEvent
    {
        public string Email { get; private set; }
        public Guid ResetPasswordSecurity { get; private set; }

        public SendResetPasswordEmailEvent(string email, Guid resetPasswordSecurity)
        {
            Email = email;
            ResetPasswordSecurity = resetPasswordSecurity;
        }
    }
}