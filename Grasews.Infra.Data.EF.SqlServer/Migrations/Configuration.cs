namespace Grasews.Infra.Data.EF.SqlServer.Migrations
{
    using global::Grasews.Domain.Entities;
    using global::Grasews.Infra.CrossCutting.Helpers;
    using System;
    using System.Data.Entity.Migrations;
    using System.IO;

    internal sealed class Configuration : DbMigrationsConfiguration<Contexts.GrasewsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Contexts.GrasewsContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Users.AddOrUpdate(
                x => x.Email,
                new User
                {
                    IsAdmin = true,
                    FirstName = "Matheus",
                    LastName = "de Lara Calache",
                    Username = "mlcalache",
                    Password = HashHelper.GetHash("123"),
                    RegistrationDateTime = DateTime.Now,
                    Email = "mlcalache@gmail.com"
                });

            //context.Users.AddOrUpdate(
            //    x => x.Email,
            //    new User
            //    {
            //        IsAdmin = true,
            //        FirstName = "Matheus",
            //        LastName = "de Lara Calache",
            //        Username = "m1",
            //        Password = HashHelper.GetHash("m1"),
            //        RegistrationDateTime = DateTime.Now,
            //        Email = "matheuscalache@usp.br"
            //    });

            //var xsdToWsdlRelationTypeValues = Enum.GetValues(typeof(XsdToWsdlRelationTypeEnum)).Cast<XsdToWsdlRelationTypeEnum>();
            //foreach (var item in xsdToWsdlRelationTypeValues)
            //{
            //    context.Database.ExecuteSqlCommand($"insert into [Grasews].[dbo].[XsdToWsdlRelationType] ([Id], [Description], [Name], [RegistrationDateTime]) values ({(int)item}, '{item.GetEnumDescription()}', '{item.ToString()}', '{DateTime.Now}')");
            //}

            //context.XsdToWsdlRelationTypes.SeedEnumValues<XsdToWsdlRelationType, XsdToWsdlRelationTypeEnum>(@enum => @enum);

            //AddServiceDescriptions(context);
            //AddOntologies(context);
        }

        private static void AddOntologies(Contexts.GrasewsContext context)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + @"\..\..\..\";

            context.Ontologies.AddOrUpdate(
                x => x.OntologyName,
                new Ontology
                {
                    OntologyName = "pizza",
                    Xml = File.ReadAllText($@"{path}\Resources\owl\pizza.owl")
                });
        }

        private static void AddServiceDescriptions(Contexts.GrasewsContext context)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + @"\..\..\..\";

            context.ServiceDescriptions.AddOrUpdate(
                x => x.ServiceName,
                new ServiceDescription
                {
                    ServiceName = "MicroAffyNormService",
                    Xml = File.ReadAllText($@"{path}\Resources\wsdl\2.0\nao anotados\MicroAffyNormService.wsdl")
                });

            context.ServiceDescriptions.AddOrUpdate(
                 x => x.ServiceName,
                 new ServiceDescription
                 {
                     ServiceName = "MicroAgilentNormService",
                     Xml = File.ReadAllText($@"{path}\Resources\wsdl\2.0\nao anotados\MicroAgilentNormService.wsdl")
                 });
        }
    }
}