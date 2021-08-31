using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCAPI.Models
{
    public class ShipContext : DbContext
    {
        public ShipContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Ship> Ship { get; set; }
    }
}
