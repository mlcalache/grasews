namespace Grasews.Domain.Entities
{
    public class GraphNodePosition_XsdSimpleType : BaseGraphNodePosition
    {
        #region Props

        public int IdOwnerUser { get; set; }
        public int IdXsdSimpleType { get; set; }

        #endregion Props

        #region Navigations

        public User OwnerUser { get; set; }
        public XsdSimpleType XsdSimpleType { get; set; }

        #endregion Navigations
    }
}