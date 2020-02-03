using System.Collections.Generic;

namespace Grasews.Domain.Entities
{
    public class XsdDocument : BaseXsd
    {
        #region Props

        public string XsdPath { get; set; }

        #endregion Props

        #region Navigations

        public virtual ServiceDescription ServiceDescription { get; set; }
        public virtual ICollection<XsdComplexType> XsdComplexTypes { get; set; }
        public virtual ICollection<XsdSimpleType> XsdSimpleTypes { get; set; }

        #endregion Navigations
    }
}