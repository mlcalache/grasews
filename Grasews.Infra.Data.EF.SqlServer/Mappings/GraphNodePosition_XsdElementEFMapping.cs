using Grasews.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.SqlServer.Mappings
{
    public class GraphNodePosition_XsdElementEFMapping : EntityTypeConfiguration<GraphNodePosition_XsdElement>
    {
        public GraphNodePosition_XsdElementEFMapping()
        {
            ToTable(nameof(GraphNodePosition_XsdElement));

            HasKey(c => c.Id);

            Property(p => p.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(GraphNodePosition_XsdElement.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(GraphNodePosition_XsdElement.RegistrationDateTime));

            HasRequired(x => x.XsdElement)
                .WithMany(x => x.GraphNodePosition_XsdElements)
                .HasForeignKey(x => x.IdXsdElement);

            HasRequired(x => x.OwnerUser)
                .WithMany(x => x.GraphNodePosition_XsdElements)
                .HasForeignKey(x => x.IdOwnerUser);
        }
    }
}