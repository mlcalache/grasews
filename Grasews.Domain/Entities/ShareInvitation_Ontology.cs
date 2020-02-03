namespace Grasews.Domain.Entities
{
    public class ShareInvitation_Ontology : BaseEntity
    {
        #region Properties

        public int IdOntology { get; set; }

        public int IdShareInvitation { get; set; }

        #endregion Properties

        #region Navigations

        public virtual Ontology Ontology { get; set; }
        public virtual ShareInvitation ShareInvitation { get; set; }

        #endregion Navigations
    }
}