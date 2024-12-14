using EcommerceWeb.Api.Models.Domain;
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

            var categories = new List <Category>() { new Category { CategoryID = 1, CategoryName = "Electronics" , Description =" Best Quality " }, new Category { CategoryID = 2, CategoryName = "Clothing" , Description =" All Size "} };


            modelBuilder.Entity<Category>().HasData(categories);







        }







    }

}
