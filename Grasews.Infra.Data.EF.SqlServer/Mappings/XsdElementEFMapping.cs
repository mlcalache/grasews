using Grasews.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.SqlServer.Mappings
{
    public class XsdElementEFMapping : EntityTypeConfiguration<XsdElement>
    {
        public XsdElementEFMapping()
        {
            ToTable(nameof(XsdElement));

            HasKey(x => x.Id);

            Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(XsdElement.Id));

            Property(x => x.XsdElementName)
                .IsRequired()
                .HasColumnName(nameof(XsdElement.XsdElementName));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(XsdElement.RegistrationDateTime));

            HasMany(e => e.Issues)
                .WithOptional(e => e.XsdElement)
                .HasForeignKey(e => e.IdXsdElement)
                .WillCascadeOnDelete(false);
        }
    }
}