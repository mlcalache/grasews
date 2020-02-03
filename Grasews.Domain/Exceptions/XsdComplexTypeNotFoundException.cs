using System;
using System.Runtime.Serialization;

namespace Grasews.Domain.Exceptions
{
    public class XsdComplexTypeNotFoundException : Exception
    {
        public XsdComplexTypeNotFoundException() : base("Xsd Complex Type not found with given Id.")
        {
        }

        public XsdComplexTypeNotFoundException(string message) : base(message)
        {
        }

        public XsdComplexTypeNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected XsdComplexTypeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}