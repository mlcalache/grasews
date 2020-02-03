using Grasews.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.SqlServer.Mappings
{
    public class SawsdlModelReferenceEFMapping : EntityTypeConfiguration<SawsdlModelReference>
    {
        public SawsdlModelReferenceEFMapping()
        {
            ToTable(nameof(SawsdlModelReference));

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
                .WillCascadeOnDelete(false);

            HasRequired(x => x.OntologyTerm)
                .WithMany(x => x.SawsdlModelReferences)
                .HasForeignKey(x => x.IdOntologyTerm)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.ServiceDescription)
                .WithMany(x => x.SawsdlModelReferences)
                .HasForeignKey(x => x.IdServiceDescription)
                .WillCascadeOnDelete(false);

            HasOptional(x => x.WsdlInFault)
                .WithMany(x => x.SawsdlModelReferences)
                .Map(x => x.MapKey("IdWsdlInFault"))
                .WillCascadeOnDelete(true);

            HasOptional(x => x.WsdlOutFault)
                .WithMany(x => x.SawsdlModelReferences)
                .Map(x => x.MapKey("IdWsdlOutFault"))
                .WillCascadeOnDelete(true);

            HasOptional(x => x.WsdlOperation)
                .WithMany(x => x.SawsdlModelReferences)
                .Map(x => x.MapKey("IdWsdlOperation"))
                .WillCascadeOnDelete(true);

            HasOptional(x => x.WsdlInterface)
                .WithMany(x => x.SawsdlModelReferences)
                .Map(x => x.MapKey("IdWsdlInterface"))
                .WillCascadeOnDelete(true);

            HasOptional(x => x.XsdComplexElement)
                .WithMany(x => x.SawsdlModelReferences)
                .Map(x => x.MapKey("IdXsdComplexElement"))
                .WillCascadeOnDelete(true);

            HasOptional(x => x.XsdElement)
                .WithMany(x => x.SawsdlModelReferences)
                .Map(x => x.MapKey("IdXsdElement"))
                .WillCascadeOnDelete(true);

            HasOptional(x => x.XsdSimpleElement)
                .WithMany(x => x.SawsdlModelReferences)
                .Map(x => x.MapKey("IdXsdSimpleElement"))
                .WillCascadeOnDelete(true);
        }
    }
}