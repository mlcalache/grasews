using System;
using System.Runtime.Serialization;

namespace Grasews.Domain.Exceptions
{
    public class SawsdlLoweringSchemaMappingAlreadyAddedException : Exception
    {
        public SawsdlLoweringSchemaMappingAlreadyAddedException() : base("SAWSDL Lowering Schema Mapping is already set.")
        {
        }

        public SawsdlLoweringSchemaMappingAlreadyAddedException(string message) : base(message)
        {
        }

        public SawsdlLoweringSchemaMappingAlreadyAddedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SawsdlLoweringSchemaMappingAlreadyAddedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}