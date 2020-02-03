using Grasews.Domain.Entities;
using Grasews.Infra.CrossCutting.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.Postgres.Mappings
{
    public class XsdComplexTypeEFMapping : EntityTypeConfiguration<XsdComplexType>
    {
        public XsdComplexTypeEFMapping()
        {
            ToTable(nameof(XsdComplexType), ConfigurationManagerHelper.DatabaseDefaultSchema);

            HasKey(x => x.Id);

            Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(XsdComplexType.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(XsdComplexType.RegistrationDateTime));

            Property(x => x.XsdComplexTypeName)
                .IsRequired()
                .HasColumnName(nameof(XsdComplexType.XsdComplexTypeName));

            Property(x => x.XsdComplexTypeElementType)
                .IsRequired()
                .HasColumnName(nameof(XsdComplexType.XsdComplexTypeElementType));

            HasMany(e => e.Issues)
                .WithOptional(e => e.XsdComplexType)
                .HasForeignKey(e => e.IdXsdComplexType)
                .WillCascadeOnDelete(true);

            HasMany(s => s.ChildrenXsdComplexTypes)
                .WithMany(c => c.ParentsXsdComplexTypes)
                .Map(cs =>
                {
                    cs.MapRightKey("IdChild");
                    cs.MapLeftKey("IdParent");
                    cs.ToTable("XsdComplexComplex");
                });
        }
    }
}