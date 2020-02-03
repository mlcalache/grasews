namespace Grasews.Infra.Data.EF.SqlServer.Contexts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WsdlOperation")]
    public partial class WsdlOperation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WsdlOperation()
        {
            Issues = new HashSet<Issue>();
            WsdlInFaults = new HashSet<WsdlInFault>();
            WsdlInputs = new HashSet<WsdlInput>();
            WsdlOutFaults = new HashSet<WsdlOutFault>();
            WsdlOutputs = new HashSet<WsdlOutput>();
        }

        public int Id { get; set; }

        public int IdWsdlInterface { get; set; }

        [Required]
        [StringLength(400)]
        public string WsdlOperationName { get; set; }

        public DateTime RegistrationDateTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Issue> Issues { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WsdlInFault> WsdlInFaults { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WsdlInput> WsdlInputs { get; set; }

        public virtual WsdlInterface WsdlInterface { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WsdlOutFault> WsdlOutFaults { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WsdlOutput> WsdlOutputs { get; set; }
    }
}
