using Grasews.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.SqlServer.Mappings
{
    public class GraphNodePosition_WsdlOperationEFMapping : EntityTypeConfiguration<GraphNodePosition_WsdlOperation>
    {
        public GraphNodePosition_WsdlOperationEFMapping()
        {
            ToTable(nameof(GraphNodePosition_WsdlOperation));

            HasKey(c => c.Id);

            Property(p => p.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(GraphNodePosition_WsdlOperation.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(GraphNodePosition_WsdlOperation.RegistrationDateTime));

            HasRequired(x => x.WsdlOperation)
                .WithMany(x => x.GraphNodePosition_WsdlOperations)
                .HasForeignKey(x => x.IdWsdlOperation);

            HasRequired(x => x.OwnerUser)
                .WithMany(x => x.GraphNodePosition_WsdlOperations)
                .HasForeignKey(x => x.IdOwnerUser);
        }
    }
}