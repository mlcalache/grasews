using Grasews.Domain.Entities;
using Grasews.Infra.CrossCutting.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.Postgres.Mappings
{
    public class GraphNodePosition_WsdlInfaultEFMapping : EntityTypeConfiguration<GraphNodePosition_WsdlInfault>
    {
        private string DatabaseDefaultSchema => ConfigurationManagerHelper.DatabaseDefaultSchema;

        public GraphNodePosition_WsdlInfaultEFMapping()
        {
            ToTable(nameof(GraphNodePosition_WsdlInfault), ConfigurationManagerHelper.DatabaseDefaultSchema);

            HasKey(c => c.Id);

            Property(p => p.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(GraphNodePosition_WsdlInfault.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(GraphNodePosition_WsdlInfault.RegistrationDateTime));

            HasRequired(x => x.WsdlInfault)
                .WithMany(x => x.GraphNodePosition_WsdlInfaults)
                .HasForeignKey(x => x.IdWsdlInfault)
                .WillCascadeOnDelete(true);

            HasRequired(x => x.OwnerUser)
                .WithMany(x => x.GraphNodePosition_WsdlInfaults)
                .HasForeignKey(x => x.IdOwnerUser)
                .WillCascadeOnDelete(true);
        }
    }
}