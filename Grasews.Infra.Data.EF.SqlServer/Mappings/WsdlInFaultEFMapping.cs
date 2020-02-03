using Grasews.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.SqlServer.Mappings
{
    public class WsdlInFaultEFMapping : EntityTypeConfiguration<WsdlInFault>
    {
        public WsdlInFaultEFMapping()
        {
            ToTable(nameof(WsdlInFault));

            HasKey(x => x.Id);

            Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(WsdlInFault.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(WsdlInFault.RegistrationDateTime));

            HasOptional(x => x.XsdComplexElement)
                .WithOptionalPrincipal(x => x.WsdlInFault)
                .Map(x => x.MapKey("IdWsdlInFault"))
                .WillCascadeOnDelete(false);
        
            HasOptional(x => x.XsdSimpleElement)
                .WithOptionalPrincipal(x => x.WsdlInFault)
                .Map(x => x.MapKey("IdWsdlInFault"))
                .WillCascadeOnDelete(false);

            HasMany(e => e.Issues)
                .WithOptional(e => e.WsdlInFault)
                .HasForeignKey(e => e.IdWsdlInFault)
                .WillCascadeOnDelete(false);
        }
    }
}