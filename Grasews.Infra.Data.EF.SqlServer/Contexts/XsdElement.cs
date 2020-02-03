namespace Grasews.Infra.Data.EF.SqlServer.Contexts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("XsdElement")]
    public partial class XsdElement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public XsdElement()
        {
            Issues = new HashSet<Issue>();
        }

        public int Id { get; set; }

        public int IdXsdComplexElement { get; set; }

        [Required]
        public string XsdElementName { get; set; }

        public string LiftingSchemaMapping { get; set; }

        public string LoweringSchemaMapping { get; set; }

        public DateTime RegistrationDateTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Issue> Issues { get; set; }

        public virtual XsdComplexElement XsdComplexElement { get; set; }
    }
}
