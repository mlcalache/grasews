using Grasews.Domain.Entities;
using Grasews.Infra.CrossCutting.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.Postgres.Mappings
{
    public class GraphNodePosition_WsdlOutfaultEFMapping : EntityTypeConfiguration<GraphNodePosition_WsdlOutfault>
    {
        public GraphNodePosition_WsdlOutfaultEFMapping()
        {
            ToTable(nameof(GraphNodePosition_WsdlOutfault), ConfigurationManagerHelper.DatabaseDefaultSchema);

            HasKey(c => c.Id);

            Property(p => p.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(GraphNodePosition_WsdlOutfault.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(GraphNodePosition_WsdlOutfault.RegistrationDateTime));

            HasRequired(x => x.WsdlOutfault)
                .WithMany(x => x.GraphNodePosition_WsdlOutfaults)
                .HasForeignKey(x => x.IdWsdlOutfault)
                .WillCascadeOnDelete(true);

            HasRequired(x => x.OwnerUser)
                .WithMany(x => x.GraphNodePosition_WsdlOutfaults)
                .HasForeignKey(x => x.IdOwnerUser)
                .WillCascadeOnDelete(true);
        }
    }
}