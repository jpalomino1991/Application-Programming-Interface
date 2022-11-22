using ClientService.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientService.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {   

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Client>()
                .HasOne(c => c.Person)
                .WithOne()
                .HasForeignKey<Client>(c => c.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
