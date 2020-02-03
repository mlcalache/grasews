namespace Grasews.Domain.Entities
{
    using System.Collections.Generic;

    public class ServiceDescription : BaseEntity<int>
    {
        #region Public props

        public string GraphJson { get; set; }
        public int IdOwnerUser { get; set; }
        public string ServiceName { get; set; }
        public string Xml { get; set; }

        #endregion Public props

        #region Navigations

        public virtual ICollection<Issue> Issues { get; set; }
        public virtual User OwnerUser { get; set; }
        public virtual ICollection<SawsdlModelReference> SawsdlModelReferences { get; set; }
        public virtual ICollection<ServiceDescription_Ontology> ServiceDescription_Ontologies { get; set; }
        public virtual ICollection<ServiceDescription_User> ServiceDescription_Users { get; set; }
        public virtual ICollection<ShareInvitation> ShareInvitations { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<WsdlInterface> WsdlInterfaces { get; set; }
        public virtual XsdDocument XsdDocument { get; set; }

        #endregion Navigations
    }
}