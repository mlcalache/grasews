using Grasews.Domain.Interfaces.DTOs;

namespace Grasews.Application.DTOs
{
    public class SawsdlModelReferenceRequestViewDTO : ISawsdlModelReferenceRequestViewDTO
    {
        public int IdServiceDescription { get; set; }
        public int IdServiceDescriptionElement { get; set; }
    }
}