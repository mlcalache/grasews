namespace Grasews.Domain.Entities
{
    public class GraphNodePosition_WsdlOutput : BaseGraphNodePosition
    {
        #region Props

        public int IdOwnerUser { get; set; }
        public int IdWsdlOutput { get; set; }

        #endregion Props

        #region Navigations

        public User OwnerUser { get; set; }
        public WsdlOutput WsdlOutput { get; set; }

        #endregion Navigations
    }
}