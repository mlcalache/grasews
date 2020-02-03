namespace Grasews.Infra.Data.EF.SqlServer.Contexts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Issue")]
    public partial class Issue
    {
        public int Id { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Description { get; set; }

        public int IdOwnerUser { get; set; }

        public int IdServiceDescription { get; set; }

        public bool Solved { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public DateTime RegistrationDateTime { get; set; }

        public int? IdWsdlInFault { get; set; }

        public int? IdWsdlOperation { get; set; }

        public int? IdWsdlInput { get; set; }

        public int? IdXsdComplexElement { get; set; }

        public int? IdWsdlOutFault { get; set; }

        public int? IdXsdSimpleElement { get; set; }

        public int? IdWsdlOutput { get; set; }

        public int? IdXsdElement { get; set; }

        public int? IdWsdlInterface { get; set; }

        public virtual ServiceDescription ServiceDescription { get; set; }

        public virtual WsdlInFault WsdlInFault { get; set; }

        public virtual WsdlInput WsdlInput { get; set; }

        public virtual WsdlInterface WsdlInterface { get; set; }

        public virtual WsdlOperation WsdlOperation { get; set; }

        public virtual WsdlOutFault WsdlOutFault { get; set; }

        public virtual WsdlOutput WsdlOutput { get; set; }

        public virtual XsdComplexElement XsdComplexElement { get; set; }

        public virtual XsdElement XsdElement { get; set; }

        public virtual XsdSimpleElement XsdSimpleElement { get; set; }
    }
}
