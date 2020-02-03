using Grasews.Domain.Entities;
using Grasews.Infra.CrossCutting.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.Postgres.Mappings
{
    public class WsdlInfaultEFMapping : EntityTypeConfiguration<WsdlInfault>
    {
        public WsdlInfaultEFMapping()
        {
            ToTable(nameof(WsdlInfault), ConfigurationManagerHelper.DatabaseDefaultSchema);

            HasKey(x => x.Id);

            Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(WsdlInfault.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(WsdlInfault.RegistrationDateTime));

            Property(x => x.WsdlInfaultName)
                .IsRequired()
                .HasMaxLength(400)
                .HasColumnName(nameof(WsdlInfault.WsdlInfaultName));

            HasOptional(x => x.XsdComplexType)
                .WithOptionalPrincipal(x => x.WsdlInfault)
                .Map(x => x.MapKey($"{nameof(WsdlInfault.Id)}{nameof(WsdlInfault)}"))
                .WillCascadeOnDelete(true);

            HasOptional(x => x.XsdSimpleType)
                .WithOptionalPrincipal(x => x.WsdlInfault)
                .Map(x => x.MapKey($"{nameof(WsdlInfault.Id)}{nameof(WsdlInfault)}"))
                .WillCascadeOnDelete(true);

            HasOptional(x => x.WsdlFault)
                .WithOptionalPrincipal(x => x.WsdlInfault)
                .Map(x => x.MapKey($"{nameof(WsdlInfault.Id)}{nameof(WsdlInfault)}"))
                .WillCascadeOnDelete(true);
        }
    }
}