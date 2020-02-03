using System;
using System.Runtime.Serialization;

namespace Grasews.Domain.Exceptions
{
    public class SawsdlModelReferenceAlreadyAddedException : Exception
    {
        public SawsdlModelReferenceAlreadyAddedException() : base("Ontology Term is already added as a SAWSDL Model Reference.")
        {
        }

        public SawsdlModelReferenceAlreadyAddedException(string message) : base(message)
        {
        }

        public SawsdlModelReferenceAlreadyAddedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SawsdlModelReferenceAlreadyAddedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}