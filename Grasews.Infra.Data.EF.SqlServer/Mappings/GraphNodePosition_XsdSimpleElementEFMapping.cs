using Grasews.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.SqlServer.Mappings
{
    public class GraphNodePosition_XsdSimpleElementEFMapping : EntityTypeConfiguration<GraphNodePosition_XsdSimpleElement>
    {
        public GraphNodePosition_XsdSimpleElementEFMapping()
        {
            ToTable(nameof(GraphNodePosition_XsdSimpleElement));

            HasKey(c => c.Id);

            Property(p => p.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(GraphNodePosition_XsdSimpleElement.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(GraphNodePosition_XsdSimpleElement.RegistrationDateTime));

            HasRequired(x => x.XsdSimpleElement)
                .WithMany(x => x.GraphNodePosition_XsdSimpleElements)
                .HasForeignKey(x => x.IdXsdSimpleElement);

            HasRequired(x => x.OwnerUser)
                .WithMany(x => x.GraphNodePosition_XsdSimpleElements)
                .HasForeignKey(x => x.IdOwnerUser);
        }
    }
}