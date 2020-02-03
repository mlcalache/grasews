using Grasews.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.SqlServer.Mappings
{
    public class WsdlOutputEFMapping : EntityTypeConfiguration<WsdlOutput>
    {
        public WsdlOutputEFMapping()
        {
            ToTable(nameof(WsdlOutput));

            HasKey(x => x.Id);

            Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(WsdlOutput.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(WsdlOutput.RegistrationDateTime));

            HasOptional(x => x.XsdComplexElement)
                .WithOptionalPrincipal(x => x.WsdlOutput)
                .Map(x => x.MapKey("IdWsdlOutput"))
                .WillCascadeOnDelete(false);

            HasOptional(x => x.XsdSimpleElement)
                .WithOptionalPrincipal(x => x.WsdlOutput)
                .Map(x => x.MapKey("IdWsdlOutput"))
                .WillCascadeOnDelete(false);

            HasMany(e => e.Issues)
                .WithOptional(e => e.WsdlOutput)
                .HasForeignKey(e => e.IdWsdlOutput)
                .WillCascadeOnDelete(false);
        }
    }
}