using Grasews.Domain.Interfaces.Entities;
using System.Collections.Generic;

namespace Grasews.Domain.Entities
{
    public class WsdlFault : BaseWsdl, ICanBeAnnotatedWithModelReference, ICanBeAnnotatedWithSchemaMapping, ICanHaveIssues
    {
        #region Props

        public int IdWsdlInterface { get; set; }
        public string WsdlFaultName { get; set; }

        #endregion Props

        #region Navigations

        public virtual ICollection<GraphNodePosition_WsdlFault> GraphNodePosition_WsdlFaults { get; set; }
        public virtual ICollection<Issue> Issues { get; set; }
        public virtual WsdlInfault WsdlInfault { get; set; }
        public virtual WsdlInterface WsdlInterface { get; set; }
        public virtual WsdlOutfault WsdlOutfault { get; set; }

        #endregion Navigations

        #region Semantic annotation

        public string LiftingSchemaMapping { get; set; }
        public string LoweringSchemaMapping { get; set; }
        public virtual ICollection<SawsdlModelReference> SawsdlModelReferences { get; set; }

        #endregion Semantic annotation

        #region ToString

        public override string ToString()
        {
            return $"[f] {WsdlFaultName}";
        }

        #endregion ToString
    }
}