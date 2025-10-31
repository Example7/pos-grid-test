using DevExpress.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DevExpress.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("now()");

            modelBuilder.Entity<Product>()
                .Property(p => p.UpdatedAt)
                .HasDefaultValueSql("now()");

            modelBuilder.Entity<Product>()
                .Property(p => p.Sku)
                .HasDefaultValueSql("gen_random_uuid()");
        }

        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries<Product>()
                .Where(e => e.State == EntityState.Modified);

            foreach (var entry in modifiedEntries)
            {
                entry.Entity.UpdatedAt = DateTime.Now;
            }

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var modifiedEntries = ChangeTracker.Entries<Product>()
                .Where(e => e.State == EntityState.Modified);

            foreach (var entry in modifiedEntries)
            {
                entry.Entity.UpdatedAt = DateTime.Now;
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
