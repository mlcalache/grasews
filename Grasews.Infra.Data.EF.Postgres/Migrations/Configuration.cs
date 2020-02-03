using Grasews.Domain.Entities;
using Grasews.Infra.CrossCutting.Helpers;
using Grasews.Infra.Data.EF.Postgres.Contexts;
using System;
using System.Data.Entity.Migrations;

namespace Grasews.Infra.Data.EF.Postgres.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<GrasewsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GrasewsContext context)
        {
            context.Users.AddOrUpdate(
                x => x.Email,
                new User
                {
                    IsAdmin = true,
                    FirstName = "Matheus",
                    LastName = "de Lara Calache",
                    Username = "mlcalache@gmail.com",
                    Password = HashHelper.GetHash("123"),
                    RegistrationDateTime = DateTime.Now,
                    Email = "mlcalache@gmail.com"
                });
        }
    }
}