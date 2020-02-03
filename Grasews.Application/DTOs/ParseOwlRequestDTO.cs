using Grasews.Domain.Interfaces.DTOs;

namespace Grasews.Application.DTOs
{
    public class ParseOwlRequestDTO : IParseOwlRequestDTO
    {
        public string Xml { get; set; }
        public string OntologyName { get; set; }
    }
}