using DemoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoAPI
{
    public class DemoApiDbContext : DbContext
    {
        public DbSet<Request> Requests { get; set; }

        public DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=demoapi.db");
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            //    mb.Entity<Request>(e =>
            //    {
            //        e.HasKey(d => d.Id);

            //        e.HasMany<Item>(f => f.Items);
            //    });

            mb.Entity<Request>()
                  .HasMany<Item>(x => x.Items)
                  .WithOne(e => e.Request);

        }

    }
}
