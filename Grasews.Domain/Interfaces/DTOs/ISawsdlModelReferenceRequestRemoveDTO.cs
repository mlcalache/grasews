using System.Collections.Generic;

namespace Grasews.Domain.Interfaces.DTOs
{
    public interface ISawsdlModelReferenceRequestRemoveDTO
    {
        IEnumerable<int> IdOntologyTerms { get; set; }
        int IdOwnerUser { get; set; }
        int IdServiceDescription { get; set; }
        int IdServiceDescriptionElement { get; set; }
    }
}