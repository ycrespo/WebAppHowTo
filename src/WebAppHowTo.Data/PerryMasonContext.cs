using Microsoft.EntityFrameworkCore;
using WebAppHowTo.Data.Model;

namespace WebAppHowTo.Data
{
    public class PerryMasonContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Practice> Practices { get; set; }

        public PerryMasonContext(DbContextOptions<PerryMasonContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var customerBuilder = modelBuilder.Entity<Customer>();

            customerBuilder.HasKey(c => c.Id);
            customerBuilder
                .HasMany(c => c.Practices)
                .WithOne(p => p.Customer)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => p.CustomerId);

            var practiceBuilder = modelBuilder.Entity<Practice>();

            practiceBuilder.HasKey(p => p.Id);
        }
    }
}