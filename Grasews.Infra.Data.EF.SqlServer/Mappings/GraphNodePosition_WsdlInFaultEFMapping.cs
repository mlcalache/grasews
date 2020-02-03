using Grasews.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.SqlServer.Mappings
{
    public class GraphNodePosition_WsdlInFaultEFMapping : EntityTypeConfiguration<GraphNodePosition_WsdlInFault>
    {
        public GraphNodePosition_WsdlInFaultEFMapping()
        {
            ToTable(nameof(GraphNodePosition_WsdlInFault));

            HasKey(c => c.Id);

            Property(p => p.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(GraphNodePosition_WsdlInFault.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(GraphNodePosition_WsdlInFault.RegistrationDateTime));

            HasRequired(x => x.WsdlInFault)
                .WithMany(x => x.GraphNodePosition_WsdlInFaults)
                .HasForeignKey(x => x.IdWsdlInFault);

            HasRequired(x => x.OwnerUser)
                .WithMany(x => x.GraphNodePosition_WsdlInFaults)
                .HasForeignKey(x => x.IdOwnerUser);
        }
    }
}