using Employ_wafi_solution.Models;
using Microsoft.EntityFrameworkCore;

namespace Employ_wafi_solution.Services
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Employ>Employs { get; set; }

    }
}
