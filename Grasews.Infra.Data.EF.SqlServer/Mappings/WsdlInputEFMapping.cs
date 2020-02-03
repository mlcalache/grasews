using Grasews.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.SqlServer.Mappings
{
    public class WsdlInputEFMapping : EntityTypeConfiguration<WsdlInput>
    {
        public WsdlInputEFMapping()
        {
            ToTable(nameof(WsdlInput));

            HasKey(x => x.Id);

            Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(WsdlInput.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(WsdlInput.RegistrationDateTime));

            HasOptional(x => x.XsdComplexElement)
                .WithOptionalPrincipal(x => x.WsdlInput)
                .Map(x => x.MapKey("IdWsdlInput"))
                .WillCascadeOnDelete(false);

            HasOptional(x => x.XsdSimpleElement)
                .WithOptionalPrincipal(x => x.WsdlInput)
                .Map(x => x.MapKey("IdWsdlInput"))
                .WillCascadeOnDelete(false);
            
            HasMany(e => e.Issues)
                .WithOptional(e => e.WsdlInput)
                .HasForeignKey(e => e.IdWsdlInput)
                .WillCascadeOnDelete(false);
        }
    }
}