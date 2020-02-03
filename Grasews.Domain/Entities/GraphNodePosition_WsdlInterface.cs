namespace Grasews.Domain.Entities
{
    public class GraphNodePosition_WsdlInterface : BaseGraphNodePosition
    {
        #region Props

        public int IdOwnerUser { get; set; }
        public int IdWsdlInterface { get; set; }

        #endregion Props

        #region Navigations

        public User OwnerUser { get; set; }
        public WsdlInterface WsdlInterface { get; set; }

        #endregion Navigations
    }
}