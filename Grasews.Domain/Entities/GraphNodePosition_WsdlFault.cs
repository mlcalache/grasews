namespace Grasews.Domain.Entities
{
    public class GraphNodePosition_WsdlFault : BaseGraphNodePosition
    {
        #region Props

        public int IdOwnerUser { get; set; }
        public int IdWsdlFault { get; set; }

        #endregion Props

        #region Navigations

        public User OwnerUser { get; set; }
        public WsdlFault WsdlFault { get; set; }

        #endregion Navigations
    }
}