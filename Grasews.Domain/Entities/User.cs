namespace Grasews.Domain.Entities
{
    using System.Collections.Generic;

    public class User : BaseEntity<int>
    {
        #region Props

        public string Email { get; set; }
        public string FirstName { get; set; }
        public bool IsAdmin { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }

        #endregion Props

        #region Navigations

        public virtual ICollection<GraphNodePosition_OntologyTerm> GraphNodePosition_OntologyTerms { get; set; }
        public virtual ICollection<GraphNodePosition_SawsdlModelReference> GraphNodePosition_SawsdlModelReferences { get; set; }
        public virtual ICollection<GraphNodePosition_WsdlFault> GraphNodePosition_WsdlFaults { get; set; }
        public virtual ICollection<GraphNodePosition_WsdlInfault> GraphNodePosition_WsdlInfaults { get; set; }
        public virtual ICollection<GraphNodePosition_WsdlInput> GraphNodePosition_WsdlInputs { get; set; }
        public virtual ICollection<GraphNodePosition_WsdlInterface> GraphNodePosition_WsdlInterfaces { get; set; }
        public virtual ICollection<GraphNodePosition_WsdlOperation> GraphNodePosition_WsdlOperations { get; set; }
        public virtual ICollection<GraphNodePosition_WsdlOutfault> GraphNodePosition_WsdlOutfaults { get; set; }
        public virtual ICollection<GraphNodePosition_WsdlOutput> GraphNodePosition_WsdlOutputs { get; set; }
        public virtual ICollection<GraphNodePosition_XsdComplexType> GraphNodePosition_XsdComplexTypes { get; set; }
        public virtual ICollection<GraphNodePosition_XsdSimpleType> GraphNodePosition_XsdSimpleTypes { get; set; }
        public virtual ICollection<IssueAnswer> IssueAnswers { get; set; }
        public virtual ICollection<Issue> Issues { get; set; }
        public virtual ICollection<Ontology> Ontologies { get; set; }
        public virtual ICollection<Ontology_User> Ontology_Users { get; set; }
        public virtual ICollection<ResetPassword> ResetPasswordRequests { get; set; }
        public virtual ICollection<SawsdlModelReference> SawsdlModelReferences { get; set; }
        public virtual ICollection<ServiceDescription_User> ServiceDescription_Users { get; set; }
        public virtual ICollection<ServiceDescription> ServiceDescriptions { get; set; }
        public virtual ICollection<ShareInvitation> ShareInvitations { get; set; }
        public virtual ICollection<TaskComment> TaskComments { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }

        #endregion Navigations

        #region Calculated props

        public string FullName => $"{FirstName} {LastName}";

        #endregion Calculated props
    }
}