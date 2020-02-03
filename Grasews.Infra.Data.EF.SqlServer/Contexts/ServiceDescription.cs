namespace Grasews.Infra.Data.EF.SqlServer.Contexts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ServiceDescription")]
    public partial class ServiceDescription
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ServiceDescription()
        {
            Issues = new HashSet<Issue>();
            WsdlInterfaces = new HashSet<WsdlInterface>();
        }

        public int Id { get; set; }

        [Column(TypeName = "text")]
        public string GraphJson { get; set; }

        public int IdOwnerUser { get; set; }

        [Required]
        [StringLength(400)]
        public string ServiceName { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Xml { get; set; }

        public DateTime RegistrationDateTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Issue> Issues { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WsdlInterface> WsdlInterfaces { get; set; }

        public virtual XsdDocument XsdDocument { get; set; }
    }
}
