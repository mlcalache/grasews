using Grasews.Domain.Entities;
using Grasews.Infra.CrossCutting.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.Postgres.Mappings
{
    public class ServiceDescriptionEFMapping : EntityTypeConfiguration<ServiceDescription>
    {
        public ServiceDescriptionEFMapping()
        {
            ToTable(nameof(ServiceDescription), ConfigurationManagerHelper.DatabaseDefaultSchema);

            HasKey(x => x.Id);

            Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(ServiceDescription.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(ServiceDescription.RegistrationDateTime));

            Property(x => x.ServiceName)
                .IsRequired()
                .HasMaxLength(400)
                .HasColumnName(nameof(ServiceDescription.ServiceName))
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute($"UQ_{nameof(ServiceDescription)}_{nameof(ServiceDescription.ServiceName)}") { IsUnique = true, Order = 1 } }));

            Property(x => x.GraphJson)
                .HasColumnType("text")
                .HasColumnName(nameof(ServiceDescription.GraphJson));

            Property(x => x.Xml)
                .IsRequired()
                .HasColumnType("text")
                .HasColumnName(nameof(ServiceDescription.Xml));

            HasMany(x => x.ServiceDescription_Users)
                .WithRequired(x => x.ServiceDescription)
                .HasForeignKey(x => x.IdServiceDescription)
                .WillCascadeOnDelete(true);

            HasMany(x => x.ServiceDescription_Ontologies)
                .WithRequired(x => x.ServiceDescription)
                .HasForeignKey(x => x.IdServiceDescription)
                .WillCascadeOnDelete(true);

            HasMany(x => x.ShareInvitations)
                .WithRequired(x => x.ServiceDescription)
                .HasForeignKey(x => x.IdServiceDescription)
                .WillCascadeOnDelete(true);

            HasMany(x => x.WsdlInterfaces)
                .WithRequired(x => x.ServiceDescription)
                .HasForeignKey(x => x.IdServiceDescription)
                .WillCascadeOnDelete(true);

            HasOptional(x => x.XsdDocument)
                .WithRequired(x => x.ServiceDescription)
                .WillCascadeOnDelete(true);
        }
    }
}