using System.Collections.Generic;
using Grasews.Domain.Entities;

namespace Grasews.Domain.Interfaces.DTOs
{
    public interface IParseOwlResponseDTO
    {
        string OntologyName { get; set; }
        List<OntologyTerm> OntologyTerms { get; set; }
        string Xml { get; set; }
    }
}