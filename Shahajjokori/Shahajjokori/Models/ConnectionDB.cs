using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Shahajjokori.Models
{
    public class ConnectionDB:DbContext
    {
        public ConnectionDB(DbContextOptions<ConnectionDB> options):base(options)
        {

        }
        public DbSet<Fundraiser> FUNDRAISERS { get; set; }
    }
}
