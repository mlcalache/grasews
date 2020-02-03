using Grasews.Domain.Entities;
using Grasews.Infra.CrossCutting.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.Postgres.Mappings
{
    public class XsdDocumentEFMapping : EntityTypeConfiguration<XsdDocument>
    {
        public XsdDocumentEFMapping()
        {
            ToTable(nameof(XsdDocument), ConfigurationManagerHelper.DatabaseDefaultSchema);

            HasKey(x => x.Id);

            Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasColumnName(nameof(XsdDocument.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(XsdDocument.RegistrationDateTime));

            HasMany(x => x.XsdComplexTypes)
                .WithRequired(x => x.XsdDocument)
                .HasForeignKey(x => x.IdXsdDocument)
                .WillCascadeOnDelete(true);

            HasMany(x => x.XsdSimpleTypes)
                .WithRequired(x => x.XsdDocument)
                .HasForeignKey(x => x.IdXsdDocument)
                .WillCascadeOnDelete(true);

            HasRequired(x => x.ServiceDescription)
                .WithOptional(x => x.XsdDocument)
                .WillCascadeOnDelete(true);
        }
    }
}