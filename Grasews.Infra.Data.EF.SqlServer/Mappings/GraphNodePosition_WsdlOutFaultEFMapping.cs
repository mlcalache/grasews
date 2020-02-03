using Grasews.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.SqlServer.Mappings
{
    public class GraphNodePosition_WsdlOutFaultEFMapping : EntityTypeConfiguration<GraphNodePosition_WsdlOutFault>
    {
        public GraphNodePosition_WsdlOutFaultEFMapping()
        {
            ToTable(nameof(GraphNodePosition_WsdlOutFault));

            HasKey(c => c.Id);

            Property(p => p.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(GraphNodePosition_WsdlOutFault.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(GraphNodePosition_WsdlOutFault.RegistrationDateTime));

            HasRequired(x => x.WsdlOutFault)
                .WithMany(x => x.GraphNodePosition_WsdlOutFaults)
                .HasForeignKey(x => x.IdWsdlOutFault);

            HasRequired(x => x.OwnerUser)
                .WithMany(x => x.GraphNodePosition_WsdlOutFaults)
                .HasForeignKey(x => x.IdOwnerUser);
        }
    }
}