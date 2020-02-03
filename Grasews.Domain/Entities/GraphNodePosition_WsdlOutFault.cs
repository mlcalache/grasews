namespace Grasews.Domain.Entities
{
    public class GraphNodePosition_WsdlOutfault : BaseGraphNodePosition
    {
        #region Props

        public int IdOwnerUser { get; set; }
        public int IdWsdlOutfault { get; set; }

        #endregion Props

        #region Navigations

        public User OwnerUser { get; set; }
        public WsdlOutfault WsdlOutfault { get; set; }

        #endregion Navigations
    }
}