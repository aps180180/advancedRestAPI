using AdvancedRestAPI.Mappings;
using AdvancedRestAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AdvancedRestAPI.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options ): base(options)
        {
                
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerMap());
        }
    }
}
