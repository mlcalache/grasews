using Grasews.Domain.Entities;
using Grasews.Infra.CrossCutting.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.Postgres.Mappings
{
    public class WsdlInterfaceEFMapping : EntityTypeConfiguration<WsdlInterface>
    {
        public WsdlInterfaceEFMapping()
        {
            ToTable(nameof(WsdlInterface), ConfigurationManagerHelper.DatabaseDefaultSchema);

            HasKey(x => x.Id);

            Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(WsdlInterface.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(WsdlInterface.RegistrationDateTime));

            Property(x => x.WsdlInterfaceName)
                .IsRequired()
                .HasMaxLength(400)
                .HasColumnName(nameof(WsdlInterface.WsdlInterfaceName));

            HasMany(x => x.WsdlOperations)
                .WithRequired(x => x.WsdlInterface)
                .HasForeignKey(x => x.IdWsdlInterface)
                .WillCascadeOnDelete(true);

            HasMany(x => x.WsdlFaults)
                .WithRequired(x => x.WsdlInterface)
                .HasForeignKey(x => x.IdWsdlInterface)
                .WillCascadeOnDelete(true);

            HasMany(e => e.Issues)
                .WithOptional(e => e.WsdlInterface)
                .HasForeignKey(e => e.IdWsdlInterface)
                .WillCascadeOnDelete(true);
        }
    }
}