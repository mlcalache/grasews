using Grasews.Domain.Entities;
using Grasews.Infra.CrossCutting.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.Postgres.Mappings
{
    public class GraphNodePosition_XsdCompleTypeEFMapping : EntityTypeConfiguration<GraphNodePosition_XsdComplexType>
    {
        public GraphNodePosition_XsdCompleTypeEFMapping()
        {
            ToTable(nameof(GraphNodePosition_XsdComplexType), ConfigurationManagerHelper.DatabaseDefaultSchema);

            HasKey(c => c.Id);

            Property(p => p.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(GraphNodePosition_XsdComplexType.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(GraphNodePosition_XsdComplexType.RegistrationDateTime));

            HasRequired(x => x.XsdComplexType)
                .WithMany(x => x.GraphNodePosition_XsdComplexTypes)
                .HasForeignKey(x => x.IdXsdComplexType);

            HasRequired(x => x.OwnerUser)
                .WithMany(x => x.GraphNodePosition_XsdComplexTypes)
                .HasForeignKey(x => x.IdOwnerUser);
        }
    }
}