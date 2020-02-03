using Grasews.Domain.Interfaces.DTOs;

namespace Grasews.Application.DTOs
{
    public class SawsdlModelReferenceRequestCreateDTO : ISawsdlModelReferenceRequestCreateDTO
    {
        public int IdOntologyTerm { get; set; }
        public int IdOwnerUser { get; set; }
        public int IdServiceDescription { get; set; }
        public int IdServiceDescriptionElement { get; set; }
    }
}