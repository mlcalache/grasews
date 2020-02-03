using Grasews.Domain.Entities;
using Grasews.Infra.CrossCutting.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.Postgres.Mappings
{
    public class SawsdlModelReferenceEFMapping : EntityTypeConfiguration<SawsdlModelReference>
    {
        public SawsdlModelReferenceEFMapping()
        {
            ToTable(nameof(SawsdlModelReference), ConfigurationManagerHelper.DatabaseDefaultSchema);

            HasKey(x => x.Id);

            Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(SawsdlModelReference.Id));

            Property(x => x.ModelReference)
                .IsRequired()
                .HasMaxLength(400)
                .HasColumnName(nameof(SawsdlModelReference.ModelReference));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(SawsdlModelReference.RegistrationDateTime));

            HasRequired(x => x.OwnerUser)
                .WithMany(x => x.SawsdlModelReferences)
                .HasForeignKey(x => x.IdOwnerUser)
                .WillCascadeOnDelete(true);

            HasRequired(x => x.OntologyTerm)
                .WithMany(x => x.SawsdlModelReferences)
                .HasForeignKey(x => x.IdOntologyTerm)
                .WillCascadeOnDelete(true);

            HasRequired(x => x.ServiceDescription)
                .WithMany(x => x.SawsdlModelReferences)
                .HasForeignKey(x => x.IdServiceDescription)
                .WillCascadeOnDelete(true);

            HasOptional(x => x.WsdlFault)
                .WithMany(x => x.SawsdlModelReferences)
                .Map(x => x.MapKey($"{nameof(WsdlFault.Id)}{nameof(WsdlFault)}"))
                .WillCascadeOnDelete(true);

            HasOptional(x => x.WsdlOperation)
                .WithMany(x => x.SawsdlModelReferences)
                .Map(x => x.MapKey($"{nameof(WsdlOperation.Id)}{nameof(WsdlOperation)}"))
                .WillCascadeOnDelete(true);

            HasOptional(x => x.WsdlInterface)
                .WithMany(x => x.SawsdlModelReferences)
                .Map(x => x.MapKey($"{nameof(WsdlInterface.Id)}{nameof(WsdlInterface)}"))
                .WillCascadeOnDelete(true);

            HasOptional(x => x.XsdComplexType)
                .WithMany(x => x.SawsdlModelReferences)
                .Map(x => x.MapKey($"{nameof(XsdComplexType.Id)}{nameof(XsdComplexType)}"))
                .WillCascadeOnDelete(true);

            HasOptional(x => x.XsdSimpleType)
                .WithMany(x => x.SawsdlModelReferences)
                .Map(x => x.MapKey($"{nameof(XsdSimpleType.Id)}{nameof(XsdSimpleType)}"))
                .WillCascadeOnDelete(true);
        }
    }
}