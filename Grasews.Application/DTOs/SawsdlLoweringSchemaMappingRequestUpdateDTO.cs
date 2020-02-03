using Grasews.Domain.Interfaces.DTOs;

namespace Grasews.Application.DTOs
{
    public class SawsdlLoweringSchemaMappingRequestUpdateDTO : ISawsdlLoweringSchemaMappingRequestUpdateDTO
    {
        public int IdServiceDescription { get; set; }
        public int IdServiceDescriptionElement { get; set; }
        public string LoweringSchemaMappingUrl { get; set; }
        public int IdOwnerUser { get; set; }
    }
}