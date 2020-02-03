using Grasews.Domain.Entities;
using Grasews.Infra.CrossCutting.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.Postgres.Mappings
{
    public class TaskCommentEFMapping : EntityTypeConfiguration<TaskComment>
    {
        public TaskCommentEFMapping()
        {
            ToTable(nameof(TaskComment), ConfigurationManagerHelper.DatabaseDefaultSchema);

            HasKey(c => c.Id);

            Property(p => p.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(TaskComment.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(TaskComment.RegistrationDateTime));

            Property(p => p.Comment)
                .IsRequired()
                .HasColumnType("text")
                .HasColumnName(nameof(TaskComment.Comment));

            HasRequired(x => x.OwnerUser)
                .WithMany(p => p.TaskComments)
                .HasForeignKey(p => p.IdOwnerUser);
        }
    }
}