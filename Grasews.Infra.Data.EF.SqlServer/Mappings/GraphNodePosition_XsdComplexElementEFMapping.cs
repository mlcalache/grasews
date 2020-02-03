using Grasews.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.SqlServer.Mappings
{
    public class GraphNodePosition_XsdComplexElementEFMapping : EntityTypeConfiguration<GraphNodePosition_XsdComplexElement>
    {
        public GraphNodePosition_XsdComplexElementEFMapping()
        {
            ToTable(nameof(GraphNodePosition_XsdComplexElement));

            HasKey(c => c.Id);

            Property(p => p.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(GraphNodePosition_XsdComplexElement.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(GraphNodePosition_XsdComplexElement.RegistrationDateTime));

            HasRequired(x => x.XsdComplexElement)
                .WithMany(x => x.GraphNodePosition_XsdComplexElements)
                .HasForeignKey(x => x.IdXsdComplexElement);

            HasRequired(x => x.OwnerUser)
                .WithMany(x => x.GraphNodePosition_XsdComplexElements)
                .HasForeignKey(x => x.IdOwnerUser);
        }
    }
}