using DemoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoAPI
{
    public class DemoApiDbContext : DbContext
    {
        public DbSet<Request> Requests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=demoapi.db");
        }
    }
}
