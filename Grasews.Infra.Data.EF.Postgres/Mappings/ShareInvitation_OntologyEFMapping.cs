using Grasews.Domain.Entities;
using Grasews.Infra.CrossCutting.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.Postgres.Mappings
{
    public class ShareInvitation_OntologyEFMapping : EntityTypeConfiguration<ShareInvitation_Ontology>
    {
        public ShareInvitation_OntologyEFMapping()
        {
            ToTable(nameof(ShareInvitation_Ontology), ConfigurationManagerHelper.DatabaseDefaultSchema);

            HasKey(c => new { c.IdShareInvitation, c.IdOntology } );

            Property(p => p.IdShareInvitation)
                .IsRequired()
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasColumnName(nameof(ShareInvitation_Ontology.IdShareInvitation));

            Property(p => p.IdOntology)
                .IsRequired()
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasColumnName(nameof(ShareInvitation_Ontology.IdOntology));

            HasRequired(x => x.ShareInvitation)
                .WithMany(p => p.ShareInvitation_Ontologies)
                .HasForeignKey(p => p.IdShareInvitation);

            HasRequired(x => x.Ontology)
                .WithMany(x => x.ShareInvitation_Ontologies)
                .HasForeignKey(x => x.IdOntology);
        }
    }
}