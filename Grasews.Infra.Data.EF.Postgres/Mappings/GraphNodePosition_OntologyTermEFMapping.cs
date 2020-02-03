using Grasews.Domain.Entities;
using Grasews.Infra.CrossCutting.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.Postgres.Mappings
{
    public class GraphNodePosition_OntologyTermEFMapping : EntityTypeConfiguration<GraphNodePosition_OntologyTerm>
    {
        public GraphNodePosition_OntologyTermEFMapping()
        {
            ToTable(nameof(GraphNodePosition_OntologyTerm), ConfigurationManagerHelper.DatabaseDefaultSchema);

            HasKey(c => c.Id);

            Property(p => p.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(GraphNodePosition_OntologyTerm.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(GraphNodePosition_OntologyTerm.RegistrationDateTime));

            HasRequired(x => x.OntologyTerm)
                .WithMany(x => x.GraphNodePosition_OntologyTerms)
                .HasForeignKey(x => x.IdOntologyTerm)
                .WillCascadeOnDelete(true);

            HasRequired(x => x.OwnerUser)
                .WithMany(x => x.GraphNodePosition_OntologyTerms)
                .HasForeignKey(x => x.IdOwnerUser)
                .WillCascadeOnDelete(true);
        }
    }
}