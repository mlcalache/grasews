using System;
using System.Runtime.Serialization;

namespace Grasews.Domain.Exceptions
{
    public class WsdlOutfaultNotFoundException : Exception
    {
        public WsdlOutfaultNotFoundException() : base("Wsdl Outfault Element not found with given Id.")
        {
        }

        public WsdlOutfaultNotFoundException(string message) : base(message)
        {
        }

        public WsdlOutfaultNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WsdlOutfaultNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}