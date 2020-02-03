using Grasews.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Grasews.Infra.Data.EF.SqlServer.Contexts
{
    public class GrasewsContext : DbContext
    {
        #region Ctors

        public GrasewsContext()
            : base("name=GrasewsContext")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

#if DEBUG
            Database.Log = s => System.Diagnostics.Trace.WriteLine(s);
            //((IObjectContextAdapter)this).ObjectContext.SavingChanges += new EventHandler(objContext_SavingChanges);
#endif
        }

        #endregion Ctors

        #region DbSets

        #region Service Descriptions and Ontologies

        public virtual DbSet<Ontology> Ontologies { get; set; }
        public virtual DbSet<OntologyTerm> OntologyTerms { get; set; }
        public virtual DbSet<ServiceDescription_Ontology> ServiceDescription_Ontologies { get; set; }
        public virtual DbSet<ServiceDescription> ServiceDescriptions { get; set; }

        #endregion Service Descriptions and Ontologies

        #region Users and Sharing

        public virtual DbSet<IssueAnswer> IssueAnswers { get; set; }
        public virtual DbSet<Issue> Issues { get; set; }
        public virtual DbSet<Ontology_User> Ontology_Users { get; set; }
        public virtual DbSet<ServiceDescription_User> ServiceDescription_Users { get; set; }
        public virtual DbSet<ShareInvitation> ShareInvitations { get; set; }
        public virtual DbSet<TaskComment> TaskComments { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<User> Users { get; set; }

        #endregion Users and Sharing

        #region Wsdl and Xsd

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

        #endregion Wsdl and Xsd

        #region Sawsdl

        public virtual DbSet<SawsdlModelReference> SawsdlModelReferences { get; set; }

        #endregion Sawsdl

        #region Graph Node Positions

        public virtual DbSet<GraphNodePosition_OntologyTerm> GraphNodePosition_OntologyTerms { get; set; }
        public virtual DbSet<GraphNodePosition_SawsdlModelReference> GraphNodePosition_SawsdlModelReferences { get; set; }
        public virtual DbSet<GraphNodePosition_WsdlInFault> GraphNodePosition_WsdlInFaults { get; set; }
        public virtual DbSet<GraphNodePosition_WsdlInput> GraphNodePosition_WsdlInputs { get; set; }
        public virtual DbSet<GraphNodePosition_WsdlInterface> GraphNodePosition_WsdlInterfaces { get; set; }
        public virtual DbSet<GraphNodePosition_WsdlOperation> GraphNodePosition_WsdlOperations { get; set; }
        public virtual DbSet<GraphNodePosition_WsdlOutFault> GraphNodePosition_WsdlOutFaults { get; set; }
        public virtual DbSet<GraphNodePosition_WsdlOutput> GraphNodePosition_WsdlOutputs { get; set; }
        public virtual DbSet<GraphNodePosition_XsdComplexElement> GraphNodePosition_XsdComplexElements { get; set; }
        public virtual DbSet<GraphNodePosition_XsdElement> GraphNodePosition_XsdElements { get; set; }
        public virtual DbSet<GraphNodePosition_XsdSimpleElement> GraphNodePosition_XsdSimpleElements { get; set; }

        #endregion Graph Node Positions

        #endregion DbSets

        #region Public methods

        public void IgnoreChanges(DbEntityEntry entry, string[] properties)
        {
            foreach (string prop in properties)
            {
                entry.Property(prop).IsModified = false;
                entry.Property(prop).CurrentValue = entry.GetDatabaseValues().GetValue<object>(prop);
            }
        }

        #endregion Public methods

        #region Events

        public void objContext_SavingChanges(object sender, EventArgs e)
        {
            var commandText = new StringBuilder();

            var conn = sender.GetType()
                 .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                 .Where(p => p.Name == "Connection")
                 .Select(p => p.GetValue(sender, null))
                 .SingleOrDefault();
            var entityConn = (EntityConnection)conn;

            var objStateManager = (System.Data.Entity.Core.Objects.ObjectStateManager)sender.GetType()
                  .GetProperty("ObjectStateManager", BindingFlags.Instance | BindingFlags.Public)
                  .GetValue(sender, null);

            var workspace = entityConn.GetMetadataWorkspace();

            var translatorT =
                sender.GetType().Assembly.GetType("System.Data.Entity.Core.Mapping.Update.Internal.UpdateTranslator");

            var entityAdapterT =
                sender.GetType().Assembly.GetType("System.Data.Entity.Core.EntityClient.Internal.EntityAdapter");
            var entityAdapter = Activator.CreateInstance(entityAdapterT, BindingFlags.Instance |
                BindingFlags.NonPublic | BindingFlags.Public, null, new object[] { sender }, System.Globalization.CultureInfo.InvariantCulture);

            entityAdapterT.GetProperty("Connection").SetValue(entityAdapter, entityConn);

            var translator = Activator.CreateInstance(translatorT, BindingFlags.Instance |
                BindingFlags.NonPublic | BindingFlags.Public, null, new object[] { entityAdapter }, System.Globalization.CultureInfo.InvariantCulture);

            var produceCommands = translator.GetType().GetMethod(
                "ProduceCommands", BindingFlags.NonPublic | BindingFlags.Instance);

            var commands = (IEnumerable<object>)produceCommands.Invoke(translator, null);

            foreach (var cmd in commands)
            {
                var identifierValues = new Dictionary<int, object>();
                var dcmd =
                    (System.Data.Common.DbCommand)cmd.GetType()
                       .GetMethod("CreateCommand", BindingFlags.Instance | BindingFlags.NonPublic)
                       .Invoke(cmd, new[] { identifierValues });

                foreach (System.Data.Common.DbParameter param in dcmd.Parameters)
                {
                    var sqlParam = (SqlParameter)param;

                    commandText.AppendLine(String.Format("declare {0} {1} {2}",
                                                            sqlParam.ParameterName,
                                                            sqlParam.SqlDbType.ToString().ToLower(),
                                                            sqlParam.Size > 0 ? "(" + sqlParam.Size + ")" : ""));

                    commandText.AppendLine(String.Format("set {0} = '{1}'", sqlParam.ParameterName, sqlParam.SqlValue));
                }

                commandText.AppendLine();
                commandText.AppendLine(dcmd.CommandText);
                commandText.AppendLine("go");
                commandText.AppendLine();
            }

            System.Diagnostics.Debug.Write(commandText.ToString());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Types().Configure(delegate (ConventionTypeConfiguration p)
            {
                if (p.ClrType.GetProperty("ValidationResult") != null)
                {
                    p.Ignore("ValidationResult");
                }
            });

            var typesMapping = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !String.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType
                    && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var mapping in typesMapping)
            {
                dynamic configurationInstance = Activator.CreateInstance(mapping);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }

        #endregion Events
    }
}