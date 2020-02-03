namespace Grasews.Infra.Data.EF.SqlServer.Contexts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("XsdComplexElement")]
    public partial class XsdComplexElement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public XsdComplexElement()
        {
            Issues = new HashSet<Issue>();
            XsdElements = new HashSet<XsdElement>();
        }

        public int Id { get; set; }

        public int IdXsdDocument { get; set; }

        public string XsdComplexElementName { get; set; }

        public string LiftingSchemaMapping { get; set; }

        public string LoweringSchemaMapping { get; set; }

        public DateTime RegistrationDateTime { get; set; }

        public int? IdWsdlOutFault { get; set; }

        public int? IdWsdlOutput { get; set; }

        public int? IdWsdlInput { get; set; }

        public int? IdWsdlInFault { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Issue> Issues { get; set; }

        public virtual WsdlInFault WsdlInFault { get; set; }

        public virtual WsdlInput WsdlInput { get; set; }

        public virtual WsdlOutFault WsdlOutFault { get; set; }

        public virtual WsdlOutput WsdlOutput { get; set; }

        public virtual XsdDocument XsdDocument { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<XsdElement> XsdElements { get; set; }
    }
}
