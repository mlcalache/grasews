using Grasews.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.SqlServer.Mappings
{
    public class ShareInvitationEFMapping : EntityTypeConfiguration<ShareInvitation>
    {
        public ShareInvitationEFMapping()
        {
            ToTable(nameof(ShareInvitation));

            HasKey(c => c.Id);

            Property(p => p.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(ShareInvitation.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(ShareInvitation.RegistrationDateTime));

            Property(x => x.Email)
                .IsRequired()
                .HasColumnName(nameof(ShareInvitation.Email));

            Property(x => x.InvitationStatus)
                .IsRequired()
                .HasColumnName(nameof(ShareInvitation.InvitationStatus));

            Property(x => x.InvitationSecurity)
                .IsRequired()
                .HasColumnType("UNIQUEIDENTIFIER")
                .HasColumnName(nameof(ShareInvitation.InvitationSecurity));

            Property(x => x.ExistingUser)
                .IsRequired()
                .HasColumnName(nameof(ShareInvitation.ExistingUser));
        }
    }
}