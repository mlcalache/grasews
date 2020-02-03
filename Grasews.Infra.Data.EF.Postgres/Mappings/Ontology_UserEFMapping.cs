using Grasews.Domain.Entities;
using Grasews.Infra.CrossCutting.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.Postgres.Mappings
{
    public class Ontology_UserEFMapping : EntityTypeConfiguration<Ontology_User>
    {
        public Ontology_UserEFMapping()
        {
            ToTable(nameof(Ontology_User), ConfigurationManagerHelper.DatabaseDefaultSchema);

            HasKey(x => x.Id);

            Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(Ontology_User.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(Ontology_User.RegistrationDateTime));

            Property(x => x.IdOntology)
                .IsRequired()
                .HasColumnName(nameof(Ontology_User.IdOntology));

            Property(x => x.IdSharedUser)
                .IsRequired()
                .HasColumnName(nameof(ServiceDescription_User.IdSharedUser));

            HasRequired(x => x.SharedUser)
                .WithMany(p => p.Ontology_Users)
                .HasForeignKey(p => p.IdSharedUser);

            HasRequired(x => x.Ontology)
                .WithMany(x => x.Ontology_Users)
                .HasForeignKey(x => x.IdOntology);
        }
    }
}