namespace Grasews.Domain.Entities
{
    public class Ontology_User : BaseEntity<int>
    {
        #region Props

        public int IdOntology { get; set; }
        public int IdSharedUser { get; set; }

        #endregion Props

        #region Navigations

        public virtual Ontology Ontology { get; set; }
        public virtual User SharedUser { get; set; }

        #endregion Navigations
    }
}