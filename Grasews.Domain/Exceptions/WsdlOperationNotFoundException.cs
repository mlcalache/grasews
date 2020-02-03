using System;
using System.Runtime.Serialization;

namespace Grasews.Domain.Exceptions
{
    public class WsdlOperationNotFoundException : Exception
    {
        public WsdlOperationNotFoundException() : base("Wsdl Operation not found with given Id.")
        {
        }

        public WsdlOperationNotFoundException(string message) : base(message)
        {
        }

        public WsdlOperationNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WsdlOperationNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}