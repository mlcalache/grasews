namespace Grasews.Domain.Entities
{
    public class GraphNodePosition_SawsdlModelReference : BaseGraphNodePosition
    {
        #region Props

        public int IdOwnerUser { get; set; }
        public int IdSawsdlModelReference { get; set; }

        #endregion Props

        #region Navigations

        public User OwnerUser { get; set; }
        public SawsdlModelReference SawsdlModelReference { get; set; }

        #endregion Navigations

        #region ToString
        public override string ToString()
        {
            return SawsdlModelReference.ModelReference;
        }
        #endregion
    }
}