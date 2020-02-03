using System;
using System.Runtime.Serialization;

namespace Grasews.Domain.Exceptions
{
    public class XsdSimpleTypeNotFoundException : Exception
    {
        public XsdSimpleTypeNotFoundException() : base("Xsd Simple Type not found with given Id.")
        {
        }

        public XsdSimpleTypeNotFoundException(string message) : base(message)
        {
        }

        public XsdSimpleTypeNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected XsdSimpleTypeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}