using System;
using System.Runtime.Serialization;

namespace Grasews.Domain.Exceptions
{
    public class OntologyTermNotFoundException : Exception
    {
        public OntologyTermNotFoundException() : base("Ontology Term not found with given Id.")
        {
        }

        public OntologyTermNotFoundException(string message) : base(message)
        {
        }

        public OntologyTermNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OntologyTermNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}