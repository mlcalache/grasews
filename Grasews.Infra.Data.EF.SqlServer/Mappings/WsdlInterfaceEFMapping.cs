using Grasews.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.SqlServer.Mappings
{
    public class WsdlInterfaceEFMapping : EntityTypeConfiguration<WsdlInterface>
    {
        public WsdlInterfaceEFMapping()
        {
            ToTable(nameof(WsdlInterface));

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
                .WillCascadeOnDelete(false);

            HasMany(e => e.Issues)
                .WithOptional(e => e.WsdlInterface)
                .HasForeignKey(e => e.IdWsdlInterface)
                .WillCascadeOnDelete(false);
        }
    }
}