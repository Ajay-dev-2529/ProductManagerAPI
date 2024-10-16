using Asst.Products.API.Extensions;
using Asst.Products.API.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Asst.Products.API.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Id)
                .ValueGeneratedNever()
                .HasValueGenerator<RandomIdValueGenerator>();


            modelBuilder.Entity<Seller>()
               .Property(s => s.Id)
               .ValueGeneratedNever()
               .HasValueGenerator<RandomIdValueGenerator>();

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Seller)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Seller> Sellers { get; set; }

    }
}
