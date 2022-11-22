using Microsoft.EntityFrameworkCore;
using MovementService.Models;

namespace MovementService.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Movement> Movements { get; set; }
    }
}
