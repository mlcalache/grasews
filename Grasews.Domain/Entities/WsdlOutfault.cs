using System.Collections.Generic;

namespace Grasews.Domain.Entities
{
    public class WsdlOutfault : BaseWsdl
    {
        #region Props

        public int IdWsdlOperation { get; set; }
        public string WsdlOutfaultName { get; set; }
        public int IdWsdlFault { get; set; }

        #endregion Props

        #region Navigations

        public virtual ICollection<GraphNodePosition_WsdlOutfault> GraphNodePosition_WsdlOutfaults { get; set; }
        public virtual WsdlOperation WsdlOperation { get; set; }
        public virtual XsdComplexType XsdComplexType { get; set; }
        public virtual XsdSimpleType XsdSimpleType { get; set; }
        public virtual WsdlFault WsdlFault { get; set; }

        #endregion Navigations

        #region ToString

        public override string ToString()
        {
            return $"[o-f] {WsdlOutfaultName}";
        }

        #endregion ToString
    }
}