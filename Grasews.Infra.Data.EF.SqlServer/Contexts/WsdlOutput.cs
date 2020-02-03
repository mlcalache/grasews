namespace Grasews.Infra.Data.EF.SqlServer.Contexts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WsdlOutput")]
    public partial class WsdlOutput
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WsdlOutput()
        {
            Issues = new HashSet<Issue>();
            XsdComplexElements = new HashSet<XsdComplexElement>();
            XsdSimpleElements = new HashSet<XsdSimpleElement>();
        }

        public int Id { get; set; }

        public int IdWsdlOperation { get; set; }

        public string WsdlOutputName { get; set; }

        public DateTime RegistrationDateTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Issue> Issues { get; set; }

        public virtual WsdlOperation WsdlOperation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<XsdComplexElement> XsdComplexElements { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<XsdSimpleElement> XsdSimpleElements { get; set; }
    }
}
