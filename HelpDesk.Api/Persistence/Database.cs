using HelpDesk.Api.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Api.Persistence
{
    public class Database : DbContext
    {
        public Database(DbContextOptions<Database> context) : base(context)
        {

        }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
