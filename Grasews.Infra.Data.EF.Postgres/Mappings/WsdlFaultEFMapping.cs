using Grasews.Domain.Entities;
using Grasews.Infra.CrossCutting.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.Postgres.Mappings
{
    public class WsdlFaultEFMapping : EntityTypeConfiguration<WsdlFault>
    {
        public WsdlFaultEFMapping()
        {
            ToTable(nameof(WsdlFault), ConfigurationManagerHelper.DatabaseDefaultSchema);

            HasKey(x => x.Id);

            Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(WsdlFault.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(WsdlFault.RegistrationDateTime));

            Property(x => x.WsdlFaultName)
                .IsRequired()
                .HasMaxLength(400)
                .HasColumnName(nameof(WsdlFault.WsdlFaultName));

            HasMany(e => e.Issues)
                .WithOptional(e => e.WsdlFault)
                .HasForeignKey(e => e.IdWsdlFault)
                .WillCascadeOnDelete(true);
        }
    }
}