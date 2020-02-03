namespace Grasews.Domain.Entities
{
    public class GraphNodePosition_XsdComplexType : BaseGraphNodePosition
    {
        #region Props

        public int IdOwnerUser { get; set; }
        public int IdXsdComplexType { get; set; }

        #endregion Props

        #region Navigations

        public User OwnerUser { get; set; }
        public XsdComplexType XsdComplexType { get; set; }

        #endregion Navigations
    }
}