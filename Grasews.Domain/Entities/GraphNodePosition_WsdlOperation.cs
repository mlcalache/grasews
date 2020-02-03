namespace Grasews.Domain.Entities
{
    public class GraphNodePosition_WsdlOperation : BaseGraphNodePosition
    {
        #region Props

        public int IdOwnerUser { get; set; }
        public int IdWsdlOperation { get; set; }

        #endregion Props

        #region Navigations

        public User OwnerUser { get; set; }
        public WsdlOperation WsdlOperation { get; set; }

        #endregion Navigations
    }
}