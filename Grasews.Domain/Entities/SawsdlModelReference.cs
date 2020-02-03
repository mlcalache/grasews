using System.Collections.Generic;

namespace Grasews.Domain.Entities
{
    public class SawsdlModelReference : BaseEntity<int>
    {
        #region Props

        public int IdOntologyTerm { get; set; }
        public int IdOwnerUser { get; set; }
        public int IdServiceDescription { get; set; }
        public string ModelReference { get; set; }

        #endregion Props

        #region Navigations

        public ICollection<GraphNodePosition_SawsdlModelReference> GraphNodePosition_SawsdlModelReferences { get; set; }
        public virtual OntologyTerm OntologyTerm { get; set; }
        public virtual User OwnerUser { get; set; }
        public virtual ServiceDescription ServiceDescription { get; set; }
        public virtual WsdlFault WsdlFault { get; set; }
        public virtual WsdlInterface WsdlInterface { get; set; }
        public virtual WsdlOperation WsdlOperation { get; set; }
        public virtual XsdComplexType XsdComplexType { get; set; }
        public virtual XsdSimpleType XsdSimpleType { get; set; }

        #endregion Navigations

        #region ToString

        public override string ToString()
        {
            return ModelReference;
        }

        #endregion ToString
    }
}