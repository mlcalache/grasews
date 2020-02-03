namespace Grasews.Infra.Data.EF.SqlServer.Contexts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WsdlInterface")]
    public partial class WsdlInterface
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WsdlInterface()
        {
            Issues = new HashSet<Issue>();
            WsdlOperations = new HashSet<WsdlOperation>();
        }

        public int Id { get; set; }

        public int IdServiceDescription { get; set; }

        [Required]
        [StringLength(400)]
        public string WsdlInterfaceName { get; set; }

        public DateTime RegistrationDateTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Issue> Issues { get; set; }

        public virtual ServiceDescription ServiceDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WsdlOperation> WsdlOperations { get; set; }
    }
}
