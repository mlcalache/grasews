namespace Grasews.Domain.Entities
{
    public class GraphNodePosition_WsdlInfault : BaseGraphNodePosition
    {
        #region Props

        public int IdOwnerUser { get; set; }
        public int IdWsdlInfault { get; set; }

        #endregion Props

        #region Navigations

        public User OwnerUser { get; set; }
        public WsdlInfault WsdlInfault { get; set; }

        #endregion Navigations
    }
}