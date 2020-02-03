using Grasews.Domain.Entities;
using Grasews.Infra.CrossCutting.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.Postgres.Mappings
{
    public class GraphNodePosition_XsdSimpleTypeEFMapping : EntityTypeConfiguration<GraphNodePosition_XsdSimpleType>
    {
        public GraphNodePosition_XsdSimpleTypeEFMapping()
        {
            ToTable(nameof(GraphNodePosition_XsdSimpleType), ConfigurationManagerHelper.DatabaseDefaultSchema);

            HasKey(c => c.Id);

            Property(p => p.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(GraphNodePosition_XsdSimpleType.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(GraphNodePosition_XsdSimpleType.RegistrationDateTime));

            HasRequired(x => x.XsdSimpleType)
                .WithMany(x => x.GraphNodePosition_XsdSimpleTypes)
                .HasForeignKey(x => x.IdXsdSimpleType);

            HasRequired(x => x.OwnerUser)
                .WithMany(x => x.GraphNodePosition_XsdSimpleTypes)
                .HasForeignKey(x => x.IdOwnerUser);
        }
    }
}