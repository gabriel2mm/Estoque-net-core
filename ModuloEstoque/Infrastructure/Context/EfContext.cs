using Microsoft.EntityFrameworkCore;
using Stock.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Infrastructure.Context
{
    public class EfContext : DbContext
    {
        public EfContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Warehouse>()
                .HasMany(w => w.ProductManagements)
                .WithOne(p => p.Warehouse)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Location>()
               .HasMany(l => l.Warehouses)
               .WithOne(w => w.Location)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Invoice>()
               .HasMany(i => i.ProductTransitions)
               .WithOne(p=> p.Invoice)
               .OnDelete(DeleteBehavior.Cascade);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductManagement> ProductControls { get; set; }
        public virtual DbSet<Provider> Providers { get; set; }
        public virtual DbSet<Warehouse> Warehouses { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<ProductTransition> ProductTransitions { get; set; }
    }
}
