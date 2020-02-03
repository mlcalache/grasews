using System;
using System.Runtime.Serialization;

namespace Grasews.Domain.Exceptions
{
    public class SawsdlModelReferenceDoesNotExistException : Exception
    {
        public SawsdlModelReferenceDoesNotExistException() : base("Ontology Term does not exist as a SAWSDL Model Reference.")
        {
        }

        public SawsdlModelReferenceDoesNotExistException(string message) : base(message)
        {
        }

        public SawsdlModelReferenceDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SawsdlModelReferenceDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}