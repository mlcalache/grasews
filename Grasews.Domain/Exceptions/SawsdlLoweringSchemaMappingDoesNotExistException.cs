using System;
using System.Runtime.Serialization;

namespace Grasews.Domain.Exceptions
{
    public class SawsdlLoweringSchemaMappingDoesNotExistException : Exception
    {
        public SawsdlLoweringSchemaMappingDoesNotExistException() : base("SASWDL Lowering Schema Mapping is not set yet.")
        {
        }

        public SawsdlLoweringSchemaMappingDoesNotExistException(string message) : base(message)
        {
        }

        public SawsdlLoweringSchemaMappingDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SawsdlLoweringSchemaMappingDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}