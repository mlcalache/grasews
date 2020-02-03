using Grasews.Domain.Entities;
using Grasews.Infra.CrossCutting.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.Postgres.Mappings
{
    public class ResetPasswordEFMapping : EntityTypeConfiguration<ResetPassword>
    {
        public ResetPasswordEFMapping()
        {
            ToTable(nameof(ResetPassword), ConfigurationManagerHelper.DatabaseDefaultSchema);

            HasKey(c => c.Id);

            Property(p => p.Id)
                 .IsRequired()
                 .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                 .HasColumnName(nameof(ResetPassword.Id));

            Property(x => x.IdUser)
                .IsRequired()
                .HasColumnName(nameof(ResetPassword.IdUser));

            Property(x => x.NewPassword)
                .HasMaxLength(50)
                .HasColumnName(nameof(ResetPassword.NewPassword));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(ResetPassword.RegistrationDateTime));

            Property(x => x.Email)
                .IsRequired()
                .HasColumnName(nameof(ResetPassword.Email));

            Property(x => x.ResetPasswordSecurity)
                .IsRequired()
                .HasColumnType("uuid")
                .HasColumnName(nameof(ResetPassword.ResetPasswordSecurity));

            Property(x => x.ResetPasswordStatus)
                .IsRequired()
                .HasColumnName(nameof(ResetPassword.ResetPasswordStatus));
        }
    }
}