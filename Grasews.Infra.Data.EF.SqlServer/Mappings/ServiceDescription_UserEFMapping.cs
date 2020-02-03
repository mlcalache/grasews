using Grasews.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.SqlServer.Mappings
{
    public class ServiceDescription_UserEFMapping : EntityTypeConfiguration<ServiceDescription_User>
    {
        public ServiceDescription_UserEFMapping()
        {
            ToTable(nameof(ServiceDescription_User));

            HasKey(x => x.Id);

            Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(ServiceDescription_User.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(ServiceDescription_User.RegistrationDateTime));

            Property(x => x.IdServiceDescription)
                .IsRequired()
                .HasColumnName(nameof(ServiceDescription_User.IdServiceDescription));

            Property(x => x.IdSharedUser)
                .IsRequired()
                .HasColumnName(nameof(ServiceDescription_User.IdSharedUser));

            HasRequired(x => x.SharedUser)
                .WithMany(p => p.ServiceDescription_Users)
                .HasForeignKey(p => p.IdSharedUser);

            HasRequired(x => x.ServiceDescription)
                .WithMany(x => x.ServiceDescription_Users)
                .HasForeignKey(x => x.IdServiceDescription);
        }
    }
}