using Grasews.Domain.Interfaces.DTOs;

namespace Grasews.Application.DTOs
{
    public class ParseWsdlRequestDTO : IParseWsdlRequestDTO
    {
        public string Xml { get; set; }
        public string ServiceName { get; set; }
    }
}