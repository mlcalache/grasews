namespace Grasews.Domain.Entities
{
    public class ServiceDescription_Ontology : BaseEntity<int>
    {
        #region Props

        public int IdOntology { get; set; }
        public int IdServiceDescription { get; set; }

        #endregion Props

        #region Navigations

        public virtual Ontology Ontology { get; set; }
        public virtual ServiceDescription ServiceDescription { get; set; }

        #endregion Navigations
    }
}