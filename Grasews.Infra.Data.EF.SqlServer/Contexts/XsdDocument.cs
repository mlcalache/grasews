namespace Grasews.Infra.Data.EF.SqlServer.Contexts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("XsdDocument")]
    public partial class XsdDocument
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public XsdDocument()
        {
            XsdComplexElements = new HashSet<XsdComplexElement>();
            XsdSimpleElements = new HashSet<XsdSimpleElement>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string XsdPath { get; set; }

        public DateTime RegistrationDateTime { get; set; }

        public virtual ServiceDescription ServiceDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<XsdComplexElement> XsdComplexElements { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<XsdSimpleElement> XsdSimpleElements { get; set; }
    }
}
