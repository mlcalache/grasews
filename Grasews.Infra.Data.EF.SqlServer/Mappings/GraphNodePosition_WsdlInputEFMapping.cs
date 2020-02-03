using Grasews.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.SqlServer.Mappings
{
    public class GraphNodePosition_WsdlInputEFMapping : EntityTypeConfiguration<GraphNodePosition_WsdlInput>
    {
        public GraphNodePosition_WsdlInputEFMapping()
        {
            ToTable(nameof(GraphNodePosition_WsdlInput));

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
                .HasForeignKey(x => x.IdWsdlInput);

            HasRequired(x => x.OwnerUser)
                .WithMany(x => x.GraphNodePosition_WsdlInputs)
                .HasForeignKey(x => x.IdOwnerUser);
        }
    }
}