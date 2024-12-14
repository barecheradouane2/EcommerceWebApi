using EcommerceWeb.Api.Data;
using EcommerceWeb.Api.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWeb.Api.Repositories
{
    public class SQLProductRepository : IProductRepository
    {
        private readonly EcommerceDbContext dbContext;

        public SQLProductRepository(EcommerceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ProductCatalog>> GetAllAsync([FromQuery] string? filterOn=null, [FromQuery] string? filterQuery = null, [FromQuery] string? sortBy=null, [FromQuery] bool? isAscending=true,  int pageNumber = 1,  int pagesize = 1000)
        {
            var products = dbContext.ProductCatalog.Include("Category").AsQueryable();

            if (string.IsNullOrEmpty(filterOn)==false &&  string.IsNullOrEmpty(filterQuery)==false)
            {
                if(filterOn == "Category")
                {
                    products = products.Where(x => x.Category.CategoryName.Contains(filterQuery));
                }
                else if (filterOn == "ProductName")
                {
                    products = products.Where(x => x.ProductName.Contains(filterQuery));
                }

            }

            if(string.IsNullOrEmpty(sortBy) == false)
            {
                if (sortBy == "Price")
                {
                    if (isAscending == true)
                    {
                        products = products.OrderBy(x => x.Price);
                    }
                    else
                    {
                        products = products.OrderByDescending(x => x.Price);
                    }
                }
                else if (sortBy == "Discount")
                {
                    if (isAscending == true)
                    {
                        products = products.OrderBy(x => x.Discount);
                    }
                    else
                    {
                        products = products.OrderByDescending(x => x.Discount);
                    }
                }
                else if (sortBy == "Stock")
                {
                    if (isAscending == true)
                    {
                        products = products.OrderBy(x => x.Stock);
                    }
                    else
                    {
                        products = products.OrderByDescending(x => x.Stock);
                    }
                }
                else if (sortBy == "CreatedAt")
                {
                    if (isAscending == true)
                    {
                        products = products.OrderBy(x => x.CreatedAt);
                    }
                    else
                    {
                        products = products.OrderByDescending(x => x.CreatedAt);
                    }
                }
            }

            //pageNumbers

            var SkipResults= (pageNumber - 1) * pagesize;

      //      products = products.Skip(SkipResults).Take(pagesize);




            return await products.Skip(SkipResults).Take(pagesize).ToListAsync();
        }


        public async Task<ProductCatalog?> GetByIdAsync(int id)
        {
            return await dbContext.ProductCatalog.Include("Category").FirstOrDefaultAsync(x=>x.ProductID==id);
        }

        public async Task<ProductCatalog> CreateAsync(ProductCatalog product)
        {
          await   dbContext.ProductCatalog.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<ProductCatalog?> UpdateAsync(int ID, ProductCatalog product)
        {
            var productToUpdate = await dbContext.ProductCatalog.Include("Category").FirstOrDefaultAsync(x=>x.ProductID==ID);
            if (productToUpdate != null)
            {
                productToUpdate.ProductName = product.ProductName;
                productToUpdate.Description = product.Description;
                productToUpdate.Price = product.Price;
                productToUpdate.Discount = product.Discount;
                productToUpdate.Stock = product.Stock;
                productToUpdate.CreatedAt = product.CreatedAt;
        
                productToUpdate.CategoryID = product.CategoryID;
                await dbContext.SaveChangesAsync();
                return productToUpdate;
            }
            return null;
        }

        public async Task<ProductCatalog?> DeleteAsync(int ID)
        {
            var productToDelete = await dbContext.ProductCatalog.Include("Category").FirstOrDefaultAsync( x=>x.ProductID== ID);
            if (productToDelete != null)
            {
                dbContext.ProductCatalog.Remove(productToDelete);
                await dbContext.SaveChangesAsync();
                return productToDelete;
            }
            return null;
        }






    }
}
