using System.Collections.Generic;

namespace Grasews.Domain.Entities
{
    public class Ontology : BaseEntity<int>
    {
        #region Props

        public string HexColor { get; set; }
        public int IdOwnerUser { get; set; }
        public string OntologyName { get; set; }
        public string OntologyUrl { get; set; }
        public string Xml { get; set; }

        #endregion Props

        #region Navigations

        public virtual ICollection<Ontology_User> Ontology_Users { get; set; }
        public virtual ICollection<OntologyTerm> OntologyTerms { get; set; }
        public virtual User OwnerUser { get; set; }
        public virtual ICollection<ServiceDescription_Ontology> ServiceDescription_Ontologies { get; set; }
        public virtual ICollection<ShareInvitation_Ontology> ShareInvitation_Ontologies { get; set; }

        #endregion Navigations
    }
}