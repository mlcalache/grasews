using System;
using System.Runtime.Serialization;

namespace Grasews.Domain.Exceptions
{
    public class SawsdlLiftingSchemaMappingAlreadyAddedException : Exception
    {
        public SawsdlLiftingSchemaMappingAlreadyAddedException() : base("SAWSDL Lifting Schema Mapping is already set.")
        {
        }

        public SawsdlLiftingSchemaMappingAlreadyAddedException(string message) : base(message)
        {
        }

        public SawsdlLiftingSchemaMappingAlreadyAddedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SawsdlLiftingSchemaMappingAlreadyAddedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}