using Grasews.Domain.Entities;
using Grasews.Infra.CrossCutting.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.Postgres.Mappings
{
    public class GraphNodePosition_WsdlInterfaceEFMapping : EntityTypeConfiguration<GraphNodePosition_WsdlInterface>
    {
        private string DatabaseDefaultSchema => ConfigurationManagerHelper.DatabaseDefaultSchema;

        public GraphNodePosition_WsdlInterfaceEFMapping()
        {
            ToTable(nameof(GraphNodePosition_WsdlInterface), ConfigurationManagerHelper.DatabaseDefaultSchema);

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
                .HasForeignKey(x => x.IdWsdlInterface)
                .WillCascadeOnDelete(true);

            HasRequired(x => x.OwnerUser)
                .WithMany(x => x.GraphNodePosition_WsdlInterfaces)
                .HasForeignKey(x => x.IdOwnerUser)
                .WillCascadeOnDelete(true);
        }
    }
}