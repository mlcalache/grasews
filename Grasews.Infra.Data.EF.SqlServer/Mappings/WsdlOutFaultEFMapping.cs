using Grasews.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.SqlServer.Mappings
{
    public class WsdlOutFaultEFMapping : EntityTypeConfiguration<WsdlOutFault>
    {
        public WsdlOutFaultEFMapping()
        {
            ToTable(nameof(WsdlOutFault));

            HasKey(x => x.Id);

            Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(WsdlOutFault.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(WsdlOutFault.RegistrationDateTime));
            
            HasOptional(x => x.XsdComplexElement)
                .WithOptionalPrincipal(x => x.WsdlOutFault)
                .Map(x => x.MapKey("IdWsdlOutFault"))
                .WillCascadeOnDelete(false);

            HasOptional(x => x.XsdSimpleElement)
                .WithOptionalPrincipal(x => x.WsdlOutFault)
                .Map(x => x.MapKey("IdWsdlOutFault"))
                .WillCascadeOnDelete(false);

            HasMany(e => e.Issues)
                .WithOptional(e => e.WsdlOutFault)
                .HasForeignKey(e => e.IdWsdlOutFault)
                .WillCascadeOnDelete(false);
        }
    }
}