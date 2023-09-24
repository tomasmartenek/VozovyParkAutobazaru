using Microsoft.EntityFrameworkCore;
using VozovyParkAutobazaru.Models;

namespace VozovyParkAutobazaru.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
    }
}