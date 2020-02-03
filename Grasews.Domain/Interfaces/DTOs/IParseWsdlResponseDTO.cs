using Grasews.Domain.Entities;

namespace Grasews.Domain.Interfaces.DTOs
{
    public interface IParseWsdlResponseDTO
    {
        ServiceDescription ServiceDescription { get; set; }
    }
}