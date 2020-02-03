using Grasews.Domain.Interfaces.DTOs;

namespace Grasews.Application.DTOs
{
    public class SawsdlLiftingSchemaMappingRequestCreateDTO : ISawsdlLiftingSchemaMappingRequestCreateDTO
    {
        public int IdServiceDescription { get; set; }
        public int IdServiceDescriptionElement { get; set; }
        public string LiftingSchemaMappingUrl { get; set; }
        public int IdOwnerUser { get; set; }
    }
}