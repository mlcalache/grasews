using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.DTOs;
using System.Collections.Generic;

namespace Grasews.Application.DTOs
{
    public class ParseOwlResponseDTO : IParseOwlResponseDTO
    {
        /// <summary>
        ///
        /// </summary>
        public string OntologyName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public List<OntologyTerm> OntologyTerms { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Xml { get; set; }
    }
}