using Grasews.Domain.Entities;
using Grasews.Infra.CrossCutting.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.Postgres.Mappings
{
    public class IssueEFMapping : EntityTypeConfiguration<Issue>
    {
        public IssueEFMapping()
        {
            ToTable(nameof(Issue), ConfigurationManagerHelper.DatabaseDefaultSchema);

            HasKey(c => c.Id);

            Property(p => p.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(Issue.Id));

            Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName(nameof(Task.Title));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(Issue.RegistrationDateTime));

            Property(p => p.Description)
                .IsRequired()
                .HasColumnType("text")
                .HasColumnName(nameof(Issue.Description));

            Property(x => x.Solved)
                .IsRequired()
                .HasColumnName(nameof(Issue.Solved));

            HasMany(x => x.IssueAnswers)
                .WithRequired(x => x.Issue)
                .HasForeignKey(x => x.IdIssue)
                .WillCascadeOnDelete(true);

            HasRequired(x => x.ServiceDescription)
                .WithMany(p => p.Issues)
                .HasForeignKey(p => p.IdServiceDescription);
        }
    }
}