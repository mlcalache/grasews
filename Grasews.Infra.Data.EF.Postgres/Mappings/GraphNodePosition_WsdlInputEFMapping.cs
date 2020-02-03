using Grasews.Domain.Entities;
using Grasews.Infra.CrossCutting.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.Postgres.Mappings
{
    public class GraphNodePosition_WsdlInputEFMapping : EntityTypeConfiguration<GraphNodePosition_WsdlInput>
    {
        private string DatabaseDefaultSchema => ConfigurationManagerHelper.DatabaseDefaultSchema;

        public GraphNodePosition_WsdlInputEFMapping()
        {
            ToTable(nameof(GraphNodePosition_WsdlInput), ConfigurationManagerHelper.DatabaseDefaultSchema);

            HasKey(c => c.Id);

            Property(p => p.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(GraphNodePosition_WsdlInput.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(GraphNodePosition_WsdlInput.RegistrationDateTime));

            HasRequired(x => x.WsdlInput)
                .WithMany(x => x.GraphNodePosition_WsdlInputs)
                .HasForeignKey(x => x.IdWsdlInput)
                .WillCascadeOnDelete(true);

            HasRequired(x => x.OwnerUser)
                .WithMany(x => x.GraphNodePosition_WsdlInputs)
                .HasForeignKey(x => x.IdOwnerUser)
                .WillCascadeOnDelete(true);
        }
    }
}