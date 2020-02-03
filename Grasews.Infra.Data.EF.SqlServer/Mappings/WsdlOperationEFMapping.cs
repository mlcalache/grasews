using Grasews.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.SqlServer.Mappings
{
    public class WsdlOperationEFMapping : EntityTypeConfiguration<WsdlOperation>
    {
        public WsdlOperationEFMapping()
        {
            ToTable(nameof(WsdlOperation));

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
                .WillCascadeOnDelete(false);

            HasMany(x => x.WsdlOutputs)
                .WithRequired(x => x.WsdlOperation)
                .HasForeignKey(x => x.IdWsdlOperation)
                .WillCascadeOnDelete(false);

            HasMany(x => x.WsdlInFaults)
                .WithRequired(x => x.WsdlOperation)
                .HasForeignKey(x => x.IdWsdlOperation)
                .WillCascadeOnDelete(false);

            HasMany(x => x.WsdlOutFaults)
                .WithRequired(x => x.WsdlOperation)
                .HasForeignKey(x => x.IdWsdlOperation)
                .WillCascadeOnDelete(false);

            HasMany(e => e.Issues)
                .WithOptional(e => e.WsdlOperation)
                .HasForeignKey(e => e.IdWsdlOperation)
                .WillCascadeOnDelete(false);
        }
    }
}