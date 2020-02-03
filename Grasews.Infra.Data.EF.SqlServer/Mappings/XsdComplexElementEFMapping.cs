using Grasews.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.SqlServer.Mappings
{
    public class XsdComplexElementEFMapping : EntityTypeConfiguration<XsdComplexElement>
    {
        public XsdComplexElementEFMapping()
        {
            ToTable(nameof(XsdComplexElement));

            HasKey(x => x.Id);

            Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(XsdComplexElement.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(XsdComplexElement.RegistrationDateTime));

            HasMany(x => x.XsdElements)
                .WithRequired(x => x.XsdComplexElement)
                .HasForeignKey(x => x.IdXsdComplexElement)
                .WillCascadeOnDelete(false);

            HasMany(e => e.Issues)
                .WithOptional(e => e.XsdComplexElement)
                .HasForeignKey(e => e.IdXsdComplexElement)
                .WillCascadeOnDelete(false);
        }
    }
}