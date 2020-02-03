namespace Grasews.Infra.Data.EF.SqlServer.Contexts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("XsdSimpleElement")]
    public partial class XsdSimpleElement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public XsdSimpleElement()
        {
            Issues = new HashSet<Issue>();
        }

        public int Id { get; set; }

        public int IdXsdDocument { get; set; }

        [Required]
        public string XsdSimpleElementName { get; set; }

        public string LiftingSchemaMapping { get; set; }

        public string LoweringSchemaMapping { get; set; }

        public DateTime RegistrationDateTime { get; set; }

        public int? IdWsdlOutput { get; set; }

        public int? IdWsdlOutFault { get; set; }

        public int? IdWsdlInput { get; set; }

        public int? IdWsdlInFault { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Issue> Issues { get; set; }

        public virtual WsdlInFault WsdlInFault { get; set; }

        public virtual WsdlInput WsdlInput { get; set; }

        public virtual WsdlOutFault WsdlOutFault { get; set; }

        public virtual WsdlOutput WsdlOutput { get; set; }

        public virtual XsdDocument XsdDocument { get; set; }
    }
}
