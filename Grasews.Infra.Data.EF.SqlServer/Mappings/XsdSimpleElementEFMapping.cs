using Grasews.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.SqlServer.Mappings
{
    public class XsdSimpleElementEFMapping : EntityTypeConfiguration<XsdSimpleElement>
    {
        public XsdSimpleElementEFMapping()
        {
            ToTable(nameof(XsdSimpleElement));

            HasKey(x => x.Id);

            Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(XsdSimpleElement.Id));

            Property(x => x.XsdSimpleElementName)
                .IsRequired()
                .HasColumnName(nameof(XsdSimpleElement.XsdSimpleElementName));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(XsdSimpleElement.RegistrationDateTime));

            HasMany(e => e.Issues)
                .WithOptional(e => e.XsdSimpleElement)
                .HasForeignKey(e => e.IdXsdSimpleElement)
                .WillCascadeOnDelete(false);
        }
    }
}