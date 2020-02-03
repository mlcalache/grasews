using System;
using System.Runtime.Serialization;

namespace Grasews.Domain.Exceptions
{
    public class SawsdlLiftingSchemaMappingDoesNotExistException : Exception
    {
        public SawsdlLiftingSchemaMappingDoesNotExistException() : base("SASWDL Lifting Schema Mapping is not set yet.")
        {
        }

        public SawsdlLiftingSchemaMappingDoesNotExistException(string message) : base(message)
        {
        }

        public SawsdlLiftingSchemaMappingDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SawsdlLiftingSchemaMappingDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}