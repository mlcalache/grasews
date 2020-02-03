namespace Grasews.Domain.Entities
{
    public class GraphNodePosition_OntologyTerm : BaseGraphNodePosition
    {
        #region Props

        public int IdOwnerUser { get; set; }
        public int IdOntologyTerm { get; set; }

        #endregion Props

        #region Navigations

        public User OwnerUser { get; set; }
        public OntologyTerm OntologyTerm { get; set; }

        #endregion Navigations
    }
}