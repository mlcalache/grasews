using Grasews.Domain.Interfaces.Entities;
using System.Collections.Generic;

namespace Grasews.Domain.Entities
{
    public class WsdlOperation : BaseWsdl, ICanBeAnnotatedWithModelReference, ICanHaveIssues
    {
        #region Props

        public int IdWsdlInterface { get; set; }
        public string WsdlOperationName { get; set; }

        #endregion Props

        #region Navigations

        public virtual ICollection<GraphNodePosition_WsdlOperation> GraphNodePosition_WsdlOperations { get; set; }
        public virtual ICollection<WsdlInfault> WsdlInfaults { get; set; }
        public virtual ICollection<WsdlInput> WsdlInputs { get; set; }
        public virtual WsdlInterface WsdlInterface { get; set; }
        public virtual ICollection<WsdlOutfault> WsdlOutfaults { get; set; }
        public virtual ICollection<WsdlOutput> WsdlOutputs { get; set; }
        public virtual ICollection<Issue> Issues { get; set; }

        #endregion Navigations

        #region Semantic annotations

        public virtual ICollection<SawsdlModelReference> SawsdlModelReferences { get; set; }

        #endregion Semantic annotations

        #region ToString

        public override string ToString()
        {
            return $"[op] {WsdlOperationName}";
        }

        #endregion ToString
    }
}