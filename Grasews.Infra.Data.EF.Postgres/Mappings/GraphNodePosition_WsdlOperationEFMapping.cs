using Grasews.Domain.Entities;
using Grasews.Infra.CrossCutting.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.Postgres.Mappings
{
    public class GraphNodePosition_WsdlOperationEFMapping : EntityTypeConfiguration<GraphNodePosition_WsdlOperation>
    {
        public GraphNodePosition_WsdlOperationEFMapping()
        {
            ToTable(nameof(GraphNodePosition_WsdlOperation), ConfigurationManagerHelper.DatabaseDefaultSchema);

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
                .HasForeignKey(x => x.IdWsdlOperation)
                .WillCascadeOnDelete(true);

            HasRequired(x => x.OwnerUser)
                .WithMany(x => x.GraphNodePosition_WsdlOperations)
                .HasForeignKey(x => x.IdOwnerUser)
                .WillCascadeOnDelete(true);
        }
    }
}