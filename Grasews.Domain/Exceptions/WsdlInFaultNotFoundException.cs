using System;
using System.Runtime.Serialization;

namespace Grasews.Domain.Exceptions
{
    public class WsdlInfaultNotFoundException : Exception
    {
        public WsdlInfaultNotFoundException() : base("Wsdl Infault Element not found with given Id.")
        {
        }

        public WsdlInfaultNotFoundException(string message) : base(message)
        {
        }

        public WsdlInfaultNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WsdlInfaultNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}