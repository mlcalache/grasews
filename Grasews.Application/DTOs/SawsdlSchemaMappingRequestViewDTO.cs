using Grasews.Domain.Interfaces.DTOs;

namespace Grasews.Application.DTOs
{
    public class SawsdlSchemaMappingRequestViewDTO : ISawsdlSchemaMappingRequestViewDTO
    {
        public int IdServiceDescription { get; set; }
        public int IdServiceDescriptionElement { get; set; }
    }
}