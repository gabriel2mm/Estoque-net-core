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
               .WithOne(p => p.Invoice)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ProductProvider>()
            .HasKey(pp => new { pp.ProductId, pp.ProviderId });

            builder.Entity<ProductProvider>()
                .HasOne(pp => pp.Product)
                .WithMany(p => p.Providers)
                .HasForeignKey(pp => pp.ProviderId);

            builder.Entity<ProductProvider>()
               .HasOne(pp => pp.Provider)
               .WithMany(p => p.Products)
               .HasForeignKey(pp => pp.ProductId);

            builder.Entity<OrdersShowcases>()
            .HasKey(os => new { os.OrderId, os.ShowcaseId });

            builder.Entity<OrdersShowcases>()
                .HasOne(os => os.Order)
                .WithMany(o => o.OrdersShowcases)
                .HasForeignKey(os => os.OrderId);

            builder.Entity<OrdersShowcases>()
               .HasOne(os => os.ShowCase)
               .WithMany(s => s.OrderShowcases)
               .HasForeignKey(os => os.ShowcaseId);

            builder.Entity<BranchOffice>()
                .HasMany(b => b.Warehouses)
                .WithOne(w => w.BranchOffice)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<BillsToPay>()
                .HasMany<BillTransaction>(b => b.BillTransactions)
                .WithOne(b => b.BillsToPay)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<BillsToReceive>()
                .HasMany<BillTransaction>(b => b.BillTransactions)
                .WithOne(b => b.BillsToReceive)
                .OnDelete(DeleteBehavior.NoAction);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        public virtual DbSet<BillsToPay> BillsToPays { get; set; }
        public virtual DbSet<BillsToReceive> BillsToReceives { get; set; }
        public virtual DbSet<BillTransaction> BillTransactions { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Campaign> Campaigns { get; set; }
        public virtual DbSet<BranchOffice> BranchOffices { get; set; }
        public virtual DbSet<OrdersShowcases> OrdersShowcases { get; set; }
        public virtual DbSet<PaymentModel> Payments { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Showcase> Showcases { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductManagement> ProductControls { get; set; }
        public virtual DbSet<Provider> Providers { get; set; }
        public virtual DbSet<Warehouse> Warehouses { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<ProductTransition> ProductTransitions { get; set; }
        public virtual DbSet<ProductProvider> ProductsProviders { get; set; }
    }
}
