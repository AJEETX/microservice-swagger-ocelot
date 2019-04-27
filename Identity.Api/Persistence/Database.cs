using Identity.Api.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Persistence
{
    public class Database : DbContext
    {
        public Database(DbContextOptions<Database> options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
    }
}
