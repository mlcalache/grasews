using System;
using System.Runtime.Serialization;

namespace Grasews.Domain.Exceptions
{
    public class UserNotAllowedException : Exception
    {
        public UserNotAllowedException() : base("User not allowed to proceed with this action.")
        {
        }

        public UserNotAllowedException(string message) : base(message)
        {
        }

        public UserNotAllowedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserNotAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}