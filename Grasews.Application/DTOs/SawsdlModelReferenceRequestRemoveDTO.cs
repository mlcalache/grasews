using Grasews.Domain.Interfaces.DTOs;
using System.Collections.Generic;

namespace Grasews.Application.DTOs
{
    public class SawsdlModelReferenceRequestRemoveDTO : ISawsdlModelReferenceRequestRemoveDTO
    {
        public IEnumerable<int> IdOntologyTerms { get; set; }
        public int IdOwnerUser { get; set; }
        public int IdServiceDescription { get; set; }
        public int IdServiceDescriptionElement { get; set; }
    }
}