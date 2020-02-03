namespace Grasews.Domain.Entities
{
    using System.Collections.Generic;

    public class Issue : BaseEntity<int>
    {
        #region Props

        public string Description { get; set; }
        public int IdOwnerUser { get; set; }
        public int IdServiceDescription { get; set; }
        public int? IdWsdlFault { get; set; }

        //public int? IdWsdlInfault { get; set; }
        //public int? IdWsdlInput { get; set; }
        public int? IdWsdlInterface { get; set; }

        public int? IdWsdlOperation { get; set; }

        //public int? IdWsdlOutfault { get; set; }
        //public int? IdWsdlOutput { get; set; }
        public int? IdXsdComplexType { get; set; }

        public int? IdXsdSimpleType { get; set; }
        public bool Solved { get; set; }
        public string Title { get; set; }

        #endregion Props

        #region Navigations

        public virtual ICollection<IssueAnswer> IssueAnswers { get; set; }
        public virtual User OwnerUser { get; set; }
        public virtual ServiceDescription ServiceDescription { get; set; }
        public virtual WsdlFault WsdlFault { get; set; }

        //public virtual WsdlInfault WsdlInfault { get; set; }
        //public virtual WsdlInput WsdlInput { get; set; }
        public virtual WsdlInterface WsdlInterface { get; set; }

        public virtual WsdlOperation WsdlOperation { get; set; }

        //public virtual WsdlOutfault WsdlOutfault { get; set; }
        //public virtual WsdlOutput WsdlOutput { get; set; }
        public virtual XsdComplexType XsdComplexType { get; set; }

        public virtual XsdSimpleType XsdSimpleType { get; set; }

        #endregion Navigations
    }
}