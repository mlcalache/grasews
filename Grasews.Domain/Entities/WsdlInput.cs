using System.Collections.Generic;

namespace Grasews.Domain.Entities
{
    public class WsdlInput : BaseWsdl
    {
        #region Props

        public int IdWsdlOperation { get; set; }
        public string WsdlInputName { get; set; }

        #endregion Props

        #region Navigations

        public virtual ICollection<GraphNodePosition_WsdlInput> GraphNodePosition_WsdlInputs { get; set; }
        public virtual WsdlOperation WsdlOperation { get; set; }
        public virtual XsdComplexType XsdComplexType { get; set; }
        public virtual XsdSimpleType XsdSimpleType { get; set; }

        #endregion Navigations

        #region ToString

        public override string ToString()
        {
            return $"[in] {WsdlInputName}";
        }

        #endregion ToString
    }
}