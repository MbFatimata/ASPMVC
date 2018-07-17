using Roomy.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Roomy.Migrations
{
    public class Configuration : DbMigrationsConfiguration<RoomyDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
        }
    }
}