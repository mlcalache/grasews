using Grasews.Domain.Entities;
using Grasews.Infra.CrossCutting.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.Postgres.Mappings
{
    public class GraphNodePosition_WsdlOutputEFMapping : EntityTypeConfiguration<GraphNodePosition_WsdlOutput>
    {
        public GraphNodePosition_WsdlOutputEFMapping()
        {
            ToTable(nameof(GraphNodePosition_WsdlOutput), ConfigurationManagerHelper.DatabaseDefaultSchema);

            HasKey(c => c.Id);

            Property(p => p.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(GraphNodePosition_WsdlOutput.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(GraphNodePosition_WsdlOutput.RegistrationDateTime));

            HasRequired(x => x.WsdlOutput)
                .WithMany(x => x.GraphNodePosition_WsdlOutputs)
                .HasForeignKey(x => x.IdWsdlOutput);

            HasRequired(x => x.OwnerUser)
                .WithMany(x => x.GraphNodePosition_WsdlOutputs)
                .HasForeignKey(x => x.IdOwnerUser);
        }
    }
}