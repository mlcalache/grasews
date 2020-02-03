using System.Collections.Generic;

namespace Grasews.Domain.Entities
{
    public class WsdlOutput : BaseWsdl
    {
        #region Props

        public int IdWsdlOperation { get; set; }
        public string WsdlOutputName { get; set; }

        #endregion Props

        #region Navigations

        public virtual ICollection<GraphNodePosition_WsdlOutput> GraphNodePosition_WsdlOutputs { get; set; }
        public virtual WsdlOperation WsdlOperation { get; set; }
        public virtual XsdComplexType XsdComplexType { get; set; }
        public virtual XsdSimpleType XsdSimpleType { get; set; }

        #endregion Navigations

        #region ToString

        public override string ToString()
        {
            return $"[out] {WsdlOutputName}";
        }

        #endregion ToString
    }
}