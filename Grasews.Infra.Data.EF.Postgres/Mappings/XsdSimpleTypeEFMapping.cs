using Grasews.Domain.Entities;
using Grasews.Infra.CrossCutting.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.Postgres.Mappings
{
    public class XsdSimpleTypeEFMapping : EntityTypeConfiguration<XsdSimpleType>
    {
        public XsdSimpleTypeEFMapping()
        {
            ToTable(nameof(XsdSimpleType), ConfigurationManagerHelper.DatabaseDefaultSchema);

            HasKey(x => x.Id);

            Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(XsdSimpleType.Id));

            Property(x => x.XsdSimpleTypeName)
                .IsRequired()
                .HasColumnName(nameof(XsdSimpleType.XsdSimpleTypeName));

            Property(x => x.XsdSimpleTypeElementType)
                .IsRequired()
                .HasColumnName(nameof(XsdSimpleType.XsdSimpleTypeElementType));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(XsdSimpleType.RegistrationDateTime));

            HasMany(e => e.Issues)
                .WithOptional(e => e.XsdSimpleType)
                .HasForeignKey(e => e.IdXsdSimpleType)
                .WillCascadeOnDelete(true);

            HasOptional(x => x.XsdComplexType)
                .WithMany(x => x.XsdSimpleTypes)                
                .Map(x => x.MapKey($"{nameof(XsdSimpleType.Id)}{nameof(XsdComplexType)}"))
                .WillCascadeOnDelete(true);
        }
    }
}