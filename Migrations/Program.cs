using Grasews.Infra.Data.EF.SqlServer.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrations
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new DbMigrationsConfiguration<GrasewsContext>();
            var databaseMigrator = new DbMigrator(configuration);
            configuration.AutomaticMigrationsEnabled = true;
            databaseMigrator.Update();
        }
    }
}
