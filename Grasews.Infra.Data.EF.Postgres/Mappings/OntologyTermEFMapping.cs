using Grasews.Domain.Entities;
using Grasews.Infra.CrossCutting.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.Postgres.Mappings
{
    public class OntologyTermEFMapping : EntityTypeConfiguration<OntologyTerm>
    {
        public OntologyTermEFMapping()
        {
            ToTable(nameof(OntologyTerm), ConfigurationManagerHelper.DatabaseDefaultSchema);

            HasKey(c => c.Id);

            Property(p => p.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(OntologyTerm.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(OntologyTerm.RegistrationDateTime));

            Property(x => x.TermName)
                .IsRequired()
                .HasMaxLength(400)
                .HasColumnName(nameof(OntologyTerm.TermName));
                //.HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_OntologyTerm_TermName") { IsUnique = false, Order = 1 } }));

            Property(x => x.TermRaw)
                .IsRequired()
                .HasColumnType("text")
                .HasColumnName(nameof(OntologyTerm.TermRaw));

            Property(x => x.TermUri)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName(nameof(OntologyTerm.TermUri));

            Ignore(x => x.ParentTermURI);

            Ignore(x => x.Depth);

            HasMany(x => x.ChildrenOntologyTerms)
                .WithOptional(x => x.ParentOntologyTerm)
                .HasForeignKey(x => x.IdParentOntologyTerm)
                .WillCascadeOnDelete(true);
        }
    }
}