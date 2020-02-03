using Grasews.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.SqlServer.Mappings
{
    public class GraphNodePosition_WsdlInterfaceEFMapping : EntityTypeConfiguration<GraphNodePosition_WsdlInterface>
    {
        public GraphNodePosition_WsdlInterfaceEFMapping()
        {
            ToTable(nameof(GraphNodePosition_WsdlInterface));

            HasKey(c => c.Id);

            Property(p => p.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(GraphNodePosition_WsdlInterface.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(GraphNodePosition_WsdlInterface.RegistrationDateTime));
            
            HasRequired(x => x.WsdlInterface)
                .WithMany(x => x.GraphNodePosition_WsdlInterfaces)
                .HasForeignKey(x => x.IdWsdlInterface);

            HasRequired(x => x.OwnerUser)
                .WithMany(x => x.GraphNodePosition_WsdlInterfaces)
                .HasForeignKey(x => x.IdOwnerUser);
        }
    }
}