using Grasews.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.SqlServer.Mappings
{
    public class XsdDocumentEFMapping : EntityTypeConfiguration<XsdDocument>
    {
        public XsdDocumentEFMapping()
        {
            ToTable(nameof(XsdDocument));

            HasKey(x => x.Id);

            Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasColumnName(nameof(XsdDocument.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(XsdDocument.RegistrationDateTime));

            HasMany(x => x.XsdComplexElements)
                .WithRequired(x => x.XsdDocument)
                .HasForeignKey(x => x.IdXsdDocument)
                .WillCascadeOnDelete(false);

            HasMany(x => x.XsdSimpleElements)
                .WithRequired(x => x.XsdDocument)
                .HasForeignKey(x => x.IdXsdDocument)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.ServiceDescription)
                .WithOptional(x => x.XsdDocument)
                .WillCascadeOnDelete(false);
        }
    }
}