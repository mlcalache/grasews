using System;
using System.Runtime.Serialization;

namespace Grasews.Domain.Exceptions
{
    public class OntologyTermListEmptyToRemoveModelReferenceException : Exception
    {
        public OntologyTermListEmptyToRemoveModelReferenceException() : base("There are no ontology terms set as model reference to be removed.")
        {
        }

        public OntologyTermListEmptyToRemoveModelReferenceException(string message) : base(message)
        {
        }

        public OntologyTermListEmptyToRemoveModelReferenceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OntologyTermListEmptyToRemoveModelReferenceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}