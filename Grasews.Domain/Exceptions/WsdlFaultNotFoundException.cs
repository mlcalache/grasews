using System;
using System.Runtime.Serialization;

namespace Grasews.Domain.Exceptions
{
    public class WsdlFaultNotFoundException : Exception
    {
        public WsdlFaultNotFoundException() : base("Wsdl Fault Element not found with given Id.")
        {
        }

        public WsdlFaultNotFoundException(string message) : base(message)
        {
        }

        public WsdlFaultNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WsdlFaultNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}