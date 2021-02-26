using Microsoft.EntityFrameworkCore;
using Shahajjokori.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shahajjokori.Context
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<PicEvent> PicEvents { get; set; }
    }
}
