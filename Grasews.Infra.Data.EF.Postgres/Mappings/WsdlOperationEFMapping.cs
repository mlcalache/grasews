using Grasews.Domain.Entities;
using Grasews.Infra.CrossCutting.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.Postgres.Mappings
{
    public class WsdlOperationEFMapping : EntityTypeConfiguration<WsdlOperation>
    {
        public WsdlOperationEFMapping()
        {
            ToTable(nameof(WsdlOperation), ConfigurationManagerHelper.DatabaseDefaultSchema);

            HasKey(x => x.Id);

            Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(WsdlOperation.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(WsdlOperation.RegistrationDateTime));

            Property(x => x.WsdlOperationName)
                .IsRequired()
                .HasMaxLength(400)
                .HasColumnName(nameof(WsdlOperation.WsdlOperationName));

            HasMany(x => x.WsdlInputs)
                .WithRequired(x => x.WsdlOperation)
                .HasForeignKey(x => x.IdWsdlOperation)
                .WillCascadeOnDelete(true);

            HasMany(x => x.WsdlOutputs)
                .WithRequired(x => x.WsdlOperation)
                .HasForeignKey(x => x.IdWsdlOperation)
                .WillCascadeOnDelete(true);

            HasMany(x => x.WsdlInfaults)
                .WithRequired(x => x.WsdlOperation)
                .HasForeignKey(x => x.IdWsdlOperation)
                .WillCascadeOnDelete(true);

            HasMany(x => x.WsdlOutfaults)
                .WithRequired(x => x.WsdlOperation)
                .HasForeignKey(x => x.IdWsdlOperation)
                .WillCascadeOnDelete(true);

            HasMany(e => e.Issues)
                .WithOptional(e => e.WsdlOperation)
                .HasForeignKey(e => e.IdWsdlOperation)
                .WillCascadeOnDelete(true);
        }
    }
}