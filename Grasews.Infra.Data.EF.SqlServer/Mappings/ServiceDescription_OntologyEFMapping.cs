using Grasews.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.SqlServer.Mappings
{
    public class ServiceDescription_OntologyEFMapping : EntityTypeConfiguration<ServiceDescription_Ontology>
    {
        public ServiceDescription_OntologyEFMapping()
        {
            ToTable(nameof(ServiceDescription_Ontology));

            HasKey(x => x.Id);

            Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(ServiceDescription_Ontology.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(ServiceDescription_Ontology.RegistrationDateTime));

            HasRequired(x => x.Ontology)
                .WithMany(p => p.ServiceDescription_Ontologies)
                .HasForeignKey(p => p.IdOntology);

            HasRequired(x => x.ServiceDescription)
                .WithMany(x => x.ServiceDescription_Ontologies)
                .HasForeignKey(x => x.IdServiceDescription);
        }
    }
}