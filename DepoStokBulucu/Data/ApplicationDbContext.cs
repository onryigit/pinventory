using DepoStokBulucu.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DepoStokBulucu.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}