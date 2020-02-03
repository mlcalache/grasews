using Grasews.Domain.Entities;
using Grasews.Infra.CrossCutting.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.Postgres.Mappings
{
    public class WsdlOutfaultEFMapping : EntityTypeConfiguration<WsdlOutfault>
    {
        public WsdlOutfaultEFMapping()
        {
            ToTable(nameof(WsdlOutfault), ConfigurationManagerHelper.DatabaseDefaultSchema);

            HasKey(x => x.Id);

            Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(WsdlOutfault.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(WsdlOutfault.RegistrationDateTime));

            Property(x => x.WsdlOutfaultName)
                .IsRequired()
                .HasMaxLength(400)
                .HasColumnName(nameof(WsdlOutfault.WsdlOutfaultName));

            HasOptional(x => x.XsdComplexType)
                .WithOptionalPrincipal(x => x.WsdlOutfault)
                .Map(x => x.MapKey($"{nameof(WsdlOutfault.Id)}{nameof(WsdlOutfault)}"))
                .WillCascadeOnDelete(true);

            HasOptional(x => x.XsdSimpleType)
                .WithOptionalPrincipal(x => x.WsdlOutfault)
                .Map(x => x.MapKey($"{nameof(WsdlOutfault.Id)}{nameof(WsdlOutfault)}"))
                .WillCascadeOnDelete(true);

            HasOptional(x => x.WsdlFault)
                .WithOptionalPrincipal(x => x.WsdlOutfault)
                .Map(x => x.MapKey($"{nameof(WsdlOutfault.Id)}{nameof(WsdlOutfault)}"))
                .WillCascadeOnDelete(true);
        }
    }
}