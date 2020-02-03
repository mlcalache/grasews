using Grasews.Domain.Entities;
using Grasews.Infra.CrossCutting.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.Postgres.Mappings
{
    public class TaskEFMapping : EntityTypeConfiguration<Task>
    {
        public TaskEFMapping()
        {
            ToTable(nameof(Task), ConfigurationManagerHelper.DatabaseDefaultSchema);

            HasKey(c => c.Id);

            Property(p => p.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(Task.Id));

            Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName(nameof(Task.Title));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(Task.RegistrationDateTime));

            Property(p => p.Done)
                .IsRequired()
                .HasColumnName(nameof(Task.Done));

            Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(1000)
                .HasColumnName(nameof(Task.Description));

            HasMany(x => x.TaskComments)
                .WithRequired(x => x.Task)
                .HasForeignKey(x => x.IdTask)
                .WillCascadeOnDelete(true);

            HasRequired(x => x.OwnerUser)
                .WithMany(p => p.Tasks)
                .HasForeignKey(p => p.IdOwnerUser);

            HasRequired(x => x.ServiceDescription)
                .WithMany(p => p.Tasks)
                .HasForeignKey(p => p.IdServiceDescription);
        }
    }
}