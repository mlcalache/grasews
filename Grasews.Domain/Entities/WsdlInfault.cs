using System.Collections.Generic;

namespace Grasews.Domain.Entities
{
    public class WsdlInfault : BaseWsdl
    {
        #region Props

        public int IdWsdlOperation { get; set; }
        public int IdWsdlFault { get; set; }
        public string WsdlInfaultName { get; set; }

        #endregion Props

        #region Navigations

        public virtual ICollection<GraphNodePosition_WsdlInfault> GraphNodePosition_WsdlInfaults { get; set; }
        public virtual WsdlOperation WsdlOperation { get; set; }
        public virtual XsdComplexType XsdComplexType { get; set; }
        public virtual XsdSimpleType XsdSimpleType { get; set; }
        public virtual WsdlFault WsdlFault { get; set; }

        #endregion Navigations

        #region ToString

        public override string ToString()
        {
            return $"[i-f] {WsdlInfaultName}";
        }

        #endregion ToString
    }
}