using System;

namespace Grasews.Domain.Exceptions
{
    public class ExternalXsdNotFoundException : Exception
    {
        public string XsdPathNotFound { get; private set; }

        public ExternalXsdNotFoundException(string xsdPathNotFound)
        {
            XsdPathNotFound = xsdPathNotFound;
        }
    }
}