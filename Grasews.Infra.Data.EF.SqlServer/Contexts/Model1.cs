namespace Grasews.Infra.Data.EF.SqlServer.Contexts
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Issue> Issues { get; set; }
        public virtual DbSet<ServiceDescription> ServiceDescriptions { get; set; }
        public virtual DbSet<WsdlInFault> WsdlInFaults { get; set; }
        public virtual DbSet<WsdlInput> WsdlInputs { get; set; }
        public virtual DbSet<WsdlInterface> WsdlInterfaces { get; set; }
        public virtual DbSet<WsdlOperation> WsdlOperations { get; set; }
        public virtual DbSet<WsdlOutFault> WsdlOutFaults { get; set; }
        public virtual DbSet<WsdlOutput> WsdlOutputs { get; set; }
        public virtual DbSet<XsdComplexElement> XsdComplexElements { get; set; }
        public virtual DbSet<XsdDocument> XsdDocuments { get; set; }
        public virtual DbSet<XsdElement> XsdElements { get; set; }
        public virtual DbSet<XsdSimpleElement> XsdSimpleElements { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Issue>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ServiceDescription>()
                .Property(e => e.GraphJson)
                .IsUnicode(false);

            modelBuilder.Entity<ServiceDescription>()
                .Property(e => e.Xml)
                .IsUnicode(false);

            modelBuilder.Entity<ServiceDescription>()
                .HasMany(e => e.Issues)
                .WithRequired(e => e.ServiceDescription)
                .HasForeignKey(e => e.IdServiceDescription);

            modelBuilder.Entity<ServiceDescription>()
                .HasMany(e => e.WsdlInterfaces)
                .WithRequired(e => e.ServiceDescription)
                .HasForeignKey(e => e.IdServiceDescription)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ServiceDescription>()
                .HasOptional(e => e.XsdDocument)
                .WithRequired(e => e.ServiceDescription);

            modelBuilder.Entity<WsdlInFault>()
                .HasMany(e => e.Issues)
                .WithOptional(e => e.WsdlInFault)
                .HasForeignKey(e => e.IdWsdlInFault);

            modelBuilder.Entity<WsdlInFault>()
                .HasMany(e => e.XsdComplexElements)
                .WithOptional(e => e.WsdlInFault)
                .HasForeignKey(e => e.IdWsdlInFault);

            modelBuilder.Entity<WsdlInFault>()
                .HasMany(e => e.XsdSimpleElements)
                .WithOptional(e => e.WsdlInFault)
                .HasForeignKey(e => e.IdWsdlInFault);

            modelBuilder.Entity<WsdlInput>()
                .HasMany(e => e.Issues)
                .WithOptional(e => e.WsdlInput)
                .HasForeignKey(e => e.IdWsdlInput);

            modelBuilder.Entity<WsdlInput>()
                .HasMany(e => e.XsdComplexElements)
                .WithOptional(e => e.WsdlInput)
                .HasForeignKey(e => e.IdWsdlInput);

            modelBuilder.Entity<WsdlInput>()
                .HasMany(e => e.XsdSimpleElements)
                .WithOptional(e => e.WsdlInput)
                .HasForeignKey(e => e.IdWsdlInput);

            modelBuilder.Entity<WsdlInterface>()
                .HasMany(e => e.Issues)
                .WithOptional(e => e.WsdlInterface)
                .HasForeignKey(e => e.IdWsdlInterface);

            modelBuilder.Entity<WsdlInterface>()
                .HasMany(e => e.WsdlOperations)
                .WithRequired(e => e.WsdlInterface)
                .HasForeignKey(e => e.IdWsdlInterface)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WsdlOperation>()
                .HasMany(e => e.Issues)
                .WithOptional(e => e.WsdlOperation)
                .HasForeignKey(e => e.IdWsdlOperation);

            modelBuilder.Entity<WsdlOperation>()
                .HasMany(e => e.WsdlInFaults)
                .WithRequired(e => e.WsdlOperation)
                .HasForeignKey(e => e.IdWsdlOperation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WsdlOperation>()
                .HasMany(e => e.WsdlInputs)
                .WithRequired(e => e.WsdlOperation)
                .HasForeignKey(e => e.IdWsdlOperation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WsdlOperation>()
                .HasMany(e => e.WsdlOutFaults)
                .WithRequired(e => e.WsdlOperation)
                .HasForeignKey(e => e.IdWsdlOperation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WsdlOperation>()
                .HasMany(e => e.WsdlOutputs)
                .WithRequired(e => e.WsdlOperation)
                .HasForeignKey(e => e.IdWsdlOperation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WsdlOutFault>()
                .HasMany(e => e.Issues)
                .WithOptional(e => e.WsdlOutFault)
                .HasForeignKey(e => e.IdWsdlOutFault);

            modelBuilder.Entity<WsdlOutFault>()
                .HasMany(e => e.XsdComplexElements)
                .WithOptional(e => e.WsdlOutFault)
                .HasForeignKey(e => e.IdWsdlOutFault);

            modelBuilder.Entity<WsdlOutFault>()
                .HasMany(e => e.XsdSimpleElements)
                .WithOptional(e => e.WsdlOutFault)
                .HasForeignKey(e => e.IdWsdlOutFault);

            modelBuilder.Entity<WsdlOutput>()
                .HasMany(e => e.Issues)
                .WithOptional(e => e.WsdlOutput)
                .HasForeignKey(e => e.IdWsdlOutput);

            modelBuilder.Entity<WsdlOutput>()
                .HasMany(e => e.XsdComplexElements)
                .WithOptional(e => e.WsdlOutput)
                .HasForeignKey(e => e.IdWsdlOutput);

            modelBuilder.Entity<WsdlOutput>()
                .HasMany(e => e.XsdSimpleElements)
                .WithOptional(e => e.WsdlOutput)
                .HasForeignKey(e => e.IdWsdlOutput);

            modelBuilder.Entity<XsdComplexElement>()
                .HasMany(e => e.Issues)
                .WithOptional(e => e.XsdComplexElement)
                .HasForeignKey(e => e.IdXsdComplexElement);

            modelBuilder.Entity<XsdComplexElement>()
                .HasMany(e => e.XsdElements)
                .WithRequired(e => e.XsdComplexElement)
                .HasForeignKey(e => e.IdXsdComplexElement)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<XsdDocument>()
                .HasMany(e => e.XsdComplexElements)
                .WithRequired(e => e.XsdDocument)
                .HasForeignKey(e => e.IdXsdDocument)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<XsdDocument>()
                .HasMany(e => e.XsdSimpleElements)
                .WithRequired(e => e.XsdDocument)
                .HasForeignKey(e => e.IdXsdDocument)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<XsdElement>()
                .HasMany(e => e.Issues)
                .WithOptional(e => e.XsdElement)
                .HasForeignKey(e => e.IdXsdElement);

            modelBuilder.Entity<XsdSimpleElement>()
                .HasMany(e => e.Issues)
                .WithOptional(e => e.XsdSimpleElement)
                .HasForeignKey(e => e.IdXsdSimpleElement);
        }
    }
}
