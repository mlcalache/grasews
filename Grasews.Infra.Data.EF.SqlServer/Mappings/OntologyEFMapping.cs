using Grasews.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.SqlServer.Mappings
{
    public class OntologyEFMapping : EntityTypeConfiguration<Ontology>
    {
        public OntologyEFMapping()
        {
            ToTable(nameof(Ontology));

            HasKey(c => c.Id);

            Property(p => p.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(Ontology.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(Ontology.RegistrationDateTime));

            Property(x => x.OntologyName)
                .IsRequired()
                .HasMaxLength(400)
                .HasColumnName(nameof(Ontology.OntologyName))
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_Ontology_OntologyName") { IsUnique = true, Order = 1 } }));

            Property(x => x.Xml)
                .IsRequired()
                .HasColumnType("text")
                .HasColumnName(nameof(Ontology.Xml));

            HasMany(x => x.ServiceDescription_Ontologies)
                .WithRequired(x => x.Ontology)
                .HasForeignKey(x => x.IdOntology)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.OwnerUser)
                .WithMany(p => p.Ontologies)
                .HasForeignKey(p => p.IdOwnerUser);

            HasMany(x => x.OntologyTerms)
                .WithOptional(x => x.Ontology)
                .HasForeignKey(x => x.IdOntology)
                .WillCascadeOnDelete(false);

            HasMany(x => x.Ontology_Users)
                .WithRequired(x => x.Ontology)
                .HasForeignKey(x => x.IdOntology)
                .WillCascadeOnDelete(false);
        }
    }
}