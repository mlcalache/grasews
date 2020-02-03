using Grasews.Domain.Entities;
using Grasews.Infra.CrossCutting.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.Postgres.Mappings
{
    public class GraphNodePosition_WsdlFaultEFMapping : EntityTypeConfiguration<GraphNodePosition_WsdlFault>
    {
        private string DatabaseDefaultSchema => ConfigurationManagerHelper.DatabaseDefaultSchema;

        public GraphNodePosition_WsdlFaultEFMapping()
        {
            ToTable(nameof(GraphNodePosition_WsdlFault), ConfigurationManagerHelper.DatabaseDefaultSchema);

            HasKey(c => c.Id);

            Property(p => p.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(GraphNodePosition_WsdlFault.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(GraphNodePosition_WsdlFault.RegistrationDateTime));

            HasRequired(x => x.WsdlFault)
                .WithMany(x => x.GraphNodePosition_WsdlFaults)
                .HasForeignKey(x => x.IdWsdlFault)
                .WillCascadeOnDelete(true);

            HasRequired(x => x.OwnerUser)
                .WithMany(x => x.GraphNodePosition_WsdlFaults)
                .HasForeignKey(x => x.IdOwnerUser)
                .WillCascadeOnDelete(true);
        }
    }
}