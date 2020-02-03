using Grasews.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.SqlServer.Mappings
{
    public class TaskEFMapping : EntityTypeConfiguration<Task>
    {
        public TaskEFMapping()
        {
            ToTable(nameof(Task));

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
                .WillCascadeOnDelete(false);

            HasRequired(x => x.OwnerUser)
                .WithMany(p => p.Tasks)
                .HasForeignKey(p => p.IdOwnerUser);

            HasRequired(x => x.ServiceDescription)
                .WithMany(p => p.Tasks)
                .HasForeignKey(p => p.IdServiceDescription);
        }
    }
}