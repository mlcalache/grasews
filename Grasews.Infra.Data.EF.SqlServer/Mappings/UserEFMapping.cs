using Grasews.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Grasews.Infra.Data.EF.SqlServer.Mappings
{
    public class UserEFMapping : EntityTypeConfiguration<User>
    {
        public UserEFMapping()
        {
            ToTable(nameof(User));

            HasKey(c => c.Id);

            Property(p => p.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(User.Id));

            Property(x => x.RegistrationDateTime)
                .IsRequired()
                .HasColumnName(nameof(User.RegistrationDateTime));

            Property(p => p.Username)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName(nameof(User.Username))
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_User_Username") { IsUnique = true, Order = 1 } }));

            Property(p => p.Password)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName(nameof(User.Password));

            Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(400)
                .HasColumnName(nameof(User.Email))
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("UQ_User_Email") { IsUnique = true, Order = 2 } }));

            Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName(nameof(User.FirstName));

            Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(400)
                .HasColumnName(nameof(User.LastName));

            HasMany(x => x.ServiceDescription_Users)
                .WithRequired(x => x.SharedUser)
                .HasForeignKey(x => x.IdSharedUser)
                .WillCascadeOnDelete(false);

            HasMany(x => x.Issues)
                .WithRequired(x => x.OwnerUser)
                .HasForeignKey(x => x.IdOwnerUser)
                .WillCascadeOnDelete(false);

            HasMany(x => x.IssueAnswers)
                .WithRequired(x => x.OwnerUser)
                .HasForeignKey(x => x.IdOwnerUser)
                .WillCascadeOnDelete(false);

            HasMany(x => x.Tasks)
                .WithRequired(x => x.OwnerUser)
                .HasForeignKey(x => x.IdOwnerUser)
                .WillCascadeOnDelete(false);

            HasMany(x => x.TaskComments)
                .WithRequired(x => x.OwnerUser)
                .HasForeignKey(x => x.IdOwnerUser)
                .WillCascadeOnDelete(false);

            HasMany(x => x.ServiceDescriptions)
                .WithRequired(x => x.OwnerUser)
                .HasForeignKey(x => x.IdOwnerUser)
                .WillCascadeOnDelete(false);

            HasMany(x => x.ShareInvitations)
                .WithRequired(x => x.UserInviter)
                .HasForeignKey(x => x.IdUserInviter)
                .WillCascadeOnDelete(false);

            HasMany(x => x.SawsdlModelReferences)
                .WithRequired(x => x.OwnerUser)
                .HasForeignKey(x => x.IdOwnerUser)
                .WillCascadeOnDelete(false);

            HasMany(x => x.GraphNodePosition_WsdlInterfaces)
                .WithRequired(x => x.OwnerUser)
                .HasForeignKey(x => x.IdOwnerUser)
                .WillCascadeOnDelete(false);

            HasMany(x => x.GraphNodePosition_WsdlOperations)
                .WithRequired(x => x.OwnerUser)
                .HasForeignKey(x => x.IdOwnerUser)
                .WillCascadeOnDelete(false);

            HasMany(x => x.Ontology_Users)
                .WithRequired(x => x.SharedUser)
                .HasForeignKey(x => x.IdSharedUser)
                .WillCascadeOnDelete(false);
        }
    }
}