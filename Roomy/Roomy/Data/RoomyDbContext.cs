using Roomy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Roomy.Data
{
    public class RoomyDbContext : DbContext
    {
        public RoomyDbContext() : base("roomyDb")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Civility> Civilities { get; set; }
    }
}