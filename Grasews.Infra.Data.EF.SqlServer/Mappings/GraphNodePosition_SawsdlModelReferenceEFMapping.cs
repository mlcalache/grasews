using Grasews.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.SqlServer.Mappings
{
    public class GraphNodePosition_SawsdlModelReferenceEFMapping : EntityTypeConfiguration<GraphNodePosition_SawsdlModelReference>
    {
        public GraphNodePosition_SawsdlModelReferenceEFMapping()
        {
            ToTable(nameof(GraphNodePosition_SawsdlModelReference));

            HasKey(c => c.Id);

            Property(p => p.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(GraphNodePosition_SawsdlModelReference.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(GraphNodePosition_SawsdlModelReference.RegistrationDateTime));

            HasRequired(x => x.SawsdlModelReference)
                .WithMany(x => x.GraphNodePosition_SawsdlModelReferences)
                .HasForeignKey(x => x.IdSawsdlModelReference);

            HasRequired(x => x.OwnerUser)
                .WithMany(x => x.GraphNodePosition_SawsdlModelReferences)
                .HasForeignKey(x => x.IdOwnerUser);
        }
    }
}