using Grasews.Domain.Entities;
using Grasews.Infra.CrossCutting.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.Postgres.Mappings
{
    public class IssueAnswerEFMapping : EntityTypeConfiguration<IssueAnswer>
    {
        public IssueAnswerEFMapping()
        {
            ToTable(nameof(IssueAnswer), ConfigurationManagerHelper.DatabaseDefaultSchema);

            HasKey(c => c.Id);

            Property(p => p.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(IssueAnswer.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(IssueAnswer.RegistrationDateTime));

            Property(p => p.Answer)
                .IsRequired()
                .HasColumnType("text")
                .HasColumnName(nameof(IssueAnswer.Answer));

            HasRequired(x => x.OwnerUser)
                .WithMany(p => p.IssueAnswers)
                .HasForeignKey(p => p.IdOwnerUser);
        }
    }
}