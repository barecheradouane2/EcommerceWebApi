﻿using EcommerceWeb.Api.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWeb.Api.Data
{
    public class EcommerceDbContext : DbContext
    {
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<EcommerceWeb.Api.Models.Domain.Category> Category { get; set; }
        public DbSet<EcommerceWeb.Api.Models.Domain.Commune> Commune { get; set; }
        public DbSet<EcommerceWeb.Api.Models.Domain.Orders> Orders { get; set; }
        public DbSet<EcommerceWeb.Api.Models.Domain.OrderItems> OrderItems { get; set; }
        public DbSet<EcommerceWeb.Api.Models.Domain.ProductCatalog> ProductCatalog { get; set; }
        public DbSet<EcommerceWeb.Api.Models.Domain.ProductImages> ProductImages { get; set; }
        public DbSet<EcommerceWeb.Api.Models.Domain.ShippingInfo> ShippingInfo { get; set; }
        public DbSet<EcommerceWeb.Api.Models.Domain.DiscountCodes> DiscountCodes { get; set; }
        public DbSet<EcommerceWeb.Api.Models.Domain.Wilaya> Wilaya { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<Orders>()
                .HasOne(o => o.Wilaya)
                .WithMany()
                .HasForeignKey(o => o.WilayaID)
                .OnDelete(DeleteBehavior.Restrict);  // Or DeleteBehavior.NoAction

            modelBuilder.Entity<Orders>()
                .HasOne(o => o.Commune)
                .WithMany()
                .HasForeignKey(o => o.CommuneID)
                .OnDelete(DeleteBehavior.Restrict);


            //  var categories = new List <Category>() { new Category {  CategoryID = 1, CategoryName = "Electronics" , Description =" Best Quality " }, new Category { CategoryID = 2, CategoryName = "Clothing" , Description =" All Size "} };

            modelBuilder.Entity<OrderItems>()
                .HasOne(oi => oi.ProductCatalog)
                .WithMany(pc => pc.OrderItems) // Only if `ProductCatalog` has `ICollection<OrderItems>`.
                .HasForeignKey(oi => oi.ProductID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>()
        .HasIndex(c => c.CategoryName)
        .IsUnique();



           // modelBuilder.Entity<Category>().HasData(categories);

            // Configure relationships between Orders and OrderItems
            modelBuilder.Entity<Orders>()
         .HasMany(o => o.OrderItems)
         .WithOne()
         .HasForeignKey(oi => oi.OrderID)
         .OnDelete(DeleteBehavior.Cascade)
         ;


            modelBuilder.Entity<Orders>()
      .HasOne(o => o.ShippingInfo)
      .WithMany()  // Assuming no navigation property on ShippingInfo side
      .HasForeignKey(o => o.ShippingID)
      .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<ProductCatalog>()
       .HasOne(pc => pc.Category) // Each ProductCatalog has one Category
       .WithMany(c => c.ProductCatalog) // Each Category has many ProductCatalogs
       .HasForeignKey(pc => pc.CategoryID) // Foreign key property in ProductCatalog
       .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<ProductImages>()
       .HasOne(pi => pi.ProductCatalog) // Each ProductImage belongs to one ProductCatalog
       .WithMany(pc => pc.ProductImages) // Each ProductCatalog has many ProductImages
       .HasForeignKey(pi => pi.ProductID) // Foreign key property in ProductImage
       .OnDelete(DeleteBehavior.Cascade);




        }







    }

}
