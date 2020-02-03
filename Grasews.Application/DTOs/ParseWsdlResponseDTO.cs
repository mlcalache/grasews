using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.DTOs;

namespace Grasews.Application.DTOs
{
    public class ParseWsdlResponseDTO : IParseWsdlResponseDTO
    {
        public ServiceDescription ServiceDescription { get; set; }
    }
}