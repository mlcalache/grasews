using Grasews.Domain.Interfaces.Entities;
using System.Collections.Generic;

namespace Grasews.Domain.Entities
{
    public class XsdSimpleType : BaseXsd, ICanBeAnnotatedWithModelReference, ICanBeAnnotatedWithSchemaMapping, ICanHaveIssues
    {
        #region Props

        public int IdXsdDocument { get; set; }
        public string XsdSimpleTypeName { get; set; }
        public string XsdSimpleTypeElementType { get; set; }

        #endregion Props

        #region Navigations

        public virtual ICollection<GraphNodePosition_XsdSimpleType> GraphNodePosition_XsdSimpleTypes { get; set; }
        public virtual WsdlInfault WsdlInfault { get; set; }
        public virtual WsdlInput WsdlInput { get; set; }
        public virtual WsdlOutfault WsdlOutfault { get; set; }
        public virtual WsdlOutput WsdlOutput { get; set; }
        public virtual XsdDocument XsdDocument { get; set; }
        public virtual XsdComplexType XsdComplexType { get; set; }
        public virtual ICollection<Issue> Issues { get; set; }

        #endregion Navigations

        #region Semantic annotations

        public string LiftingSchemaMapping { get; set; }
        public string LoweringSchemaMapping { get; set; }
        public virtual ICollection<SawsdlModelReference> SawsdlModelReferences { get; set; }

        #endregion Semantic annotations

        #region ToString

        public override string ToString()
        {
            return $"[s-t] {XsdSimpleTypeName}";
        }

        #endregion ToString
    }
}