using Grasews.Domain.Entities;
using Grasews.Infra.CrossCutting.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.Postgres.Mappings
{
    public class WsdlOutputEFMapping : EntityTypeConfiguration<WsdlOutput>
    {
        public WsdlOutputEFMapping()
        {
            ToTable(nameof(WsdlOutput), ConfigurationManagerHelper.DatabaseDefaultSchema);

            HasKey(x => x.Id);

            Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(WsdlOutput.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(WsdlOutput.RegistrationDateTime));

            HasOptional(x => x.XsdComplexType)
                .WithOptionalPrincipal(x => x.WsdlOutput)
                .Map(x => x.MapKey($"{nameof(WsdlOutput.Id)}{nameof(WsdlOutput)}"))
                .WillCascadeOnDelete(true);

            HasOptional(x => x.XsdSimpleType)
                .WithOptionalPrincipal(x => x.WsdlOutput)
                .Map(x => x.MapKey($"{nameof(WsdlOutput.Id)}{nameof(WsdlOutput)}"))
                .WillCascadeOnDelete(true);
        }
    }
}