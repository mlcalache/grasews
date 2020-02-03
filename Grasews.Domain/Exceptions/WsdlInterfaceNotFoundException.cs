using System;
using System.Runtime.Serialization;

namespace Grasews.Domain.Exceptions
{
    public class WsdlInterfaceNotFoundException : Exception
    {
        public WsdlInterfaceNotFoundException() : base("Wsdl Interface not found with given Id.")
        {
        }

        public WsdlInterfaceNotFoundException(string message) : base(message)
        {
        }

        public WsdlInterfaceNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WsdlInterfaceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}