using Grasews.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.SqlServer.Mappings
{

    public class GraphNodePosition_OntologyTermEFMapping : EntityTypeConfiguration<GraphNodePosition_OntologyTerm>
    {
        public GraphNodePosition_OntologyTermEFMapping()
        {
            ToTable(nameof(GraphNodePosition_OntologyTerm));

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
                .HasForeignKey(x => x.IdOntologyTerm);

            HasRequired(x => x.OwnerUser)
                .WithMany(x => x.GraphNodePosition_OntologyTerms)
                .HasForeignKey(x => x.IdOwnerUser);
        }
    }
}