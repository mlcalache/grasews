namespace Grasews.Domain.Entities
{
    public class GraphNodePosition_WsdlInput : BaseGraphNodePosition
    {
        #region Props

        public int IdOwnerUser { get; set; }
        public int IdWsdlInput { get; set; }

        #endregion Props

        #region Navigations

        public User OwnerUser { get; set; }
        public WsdlInput WsdlInput { get; set; }

        #endregion Navigations
    }
}