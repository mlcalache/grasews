using Grasews.Domain.Entities;
using Grasews.Infra.CrossCutting.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.Postgres.Mappings
{
    public class WsdlInputEFMapping : EntityTypeConfiguration<WsdlInput>
    {
        public WsdlInputEFMapping()
        {
            ToTable(nameof(WsdlInput), ConfigurationManagerHelper.DatabaseDefaultSchema);

            HasKey(x => x.Id);

            Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(WsdlInput.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(WsdlInput.RegistrationDateTime));

            HasOptional(x => x.XsdComplexType)
                .WithOptionalPrincipal(x => x.WsdlInput)
                .Map(x => x.MapKey($"{nameof(WsdlInput.Id)}{nameof(WsdlInput)}"))
                .WillCascadeOnDelete(true);

            HasOptional(x => x.XsdSimpleType)
                .WithOptionalPrincipal(x => x.WsdlInput)
                .Map(x => x.MapKey($"{nameof(WsdlInput.Id)}{nameof(WsdlInput)}"))
                .WillCascadeOnDelete(true);
        }
    }
}