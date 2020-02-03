using System;
using System.Runtime.Serialization;

namespace Grasews.Domain.Exceptions
{
    public class ServiceDescriptionNotSharedWithUserException : Exception
    {
        public ServiceDescriptionNotSharedWithUserException() : base("Service description not shared with user.")
        {
        }

        public ServiceDescriptionNotSharedWithUserException(string message) : base(message)
        {
        }

        public ServiceDescriptionNotSharedWithUserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ServiceDescriptionNotSharedWithUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}