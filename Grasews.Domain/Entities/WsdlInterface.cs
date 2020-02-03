using Grasews.Domain.Interfaces.Entities;
using System.Collections.Generic;

namespace Grasews.Domain.Entities
{
    public class WsdlInterface : BaseWsdl, ICanBeAnnotatedWithModelReference, ICanHaveIssues
    {
        #region Props

        public int IdServiceDescription { get; set; }
        public string WsdlInterfaceName { get; set; }

        #endregion Props

        #region Navigations

        public virtual ICollection<GraphNodePosition_WsdlInterface> GraphNodePosition_WsdlInterfaces { get; set; }
        public virtual ServiceDescription ServiceDescription { get; set; }
        public virtual ICollection<WsdlOperation> WsdlOperations { get; set; }
        public virtual ICollection<WsdlFault> WsdlFaults { get; set; }
        public virtual ICollection<Issue> Issues { get; set; }

        #endregion Navigations

        #region Semantic annotations

        public virtual ICollection<SawsdlModelReference> SawsdlModelReferences { get; set; }

        #endregion Semantic annotations

        #region ToString

        public override string ToString()
        {
            return $"[i] {WsdlInterfaceName}";
        }

        #endregion ToString
    }
}