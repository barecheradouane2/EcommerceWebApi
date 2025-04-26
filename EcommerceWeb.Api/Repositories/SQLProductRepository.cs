using EcommerceWeb.Api.Data;
using EcommerceWeb.Api.Models.Domain;
using EcommerceWeb.Api.Models.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace EcommerceWeb.Api.Repositories
{
    public class SQLProductRepository : IProductRepository
    {
        private readonly EcommerceDbContext dbContext;

        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public SQLProductRepository(EcommerceDbContext dbContext, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this.dbContext = dbContext;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<PagedResult<ProductCatalog>> GetAllAsync([FromQuery] string? filterOn=null, [FromQuery] string? filterQuery = null, [FromQuery] string? sortBy=null, [FromQuery] bool? isAscending=true,  int pageNumber = 1,  int pagesize = 9)
        {


            var products = dbContext.ProductCatalog.Include(pc => pc.ProductImages).Include(pc => pc.ProductSizes).ThenInclude(ps => ps.ProductColorVariant).AsQueryable();

          


            



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


            int totalCount = await products.CountAsync();
            int totalPages = (int)Math.Ceiling(totalCount / (double)pagesize);


            var SkipResults= (pageNumber - 1) * pagesize;
            var pagedItems = await products.Skip(SkipResults).Take(pagesize).ToListAsync();




            return new PagedResult<ProductCatalog>
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                PageNumber = pageNumber,
                PageSize = pagesize,
                Data = pagedItems
            }; ;
        }


        public async Task<ProductCatalog?> GetByIdAsync(int id)
        {

            return await dbContext.ProductCatalog.Include(pc => pc.ProductImages).Include(pc => pc.ProductSizes).ThenInclude(ps => ps.ProductColorVariant)
    .FirstOrDefaultAsync(x => x.ProductID == id);




        }

        public async Task<ProductCatalog> CreateAsync(ProductCatalog product)
        {

            

            foreach (var productImage in product.ProductImages)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(productImage.ImageFile.FileName)}";
                var localfilepath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", fileName);

                using var stream = new FileStream(localfilepath, FileMode.Create);

                await productImage.ImageFile.CopyToAsync(stream);

               
                productImage.ImageUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{fileName}";
            }




            //try
            //{


                await dbContext.ProductCatalog.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return product;

            //}
            //catch (DbUpdateException ex)
            //{

            //    return null;
            
               

            //}

          }

        public async Task<ProductCatalog?> UpdateAsync(int ID, ProductCatalog product)
        {

          

            var productToUpdate =  await dbContext.ProductCatalog.Include(pc => pc.Category).Include(pc => pc.ProductImages).Include(pc => pc.ProductSizes).ThenInclude(ps => ps.ProductColorVariant).FirstOrDefaultAsync(x => x.ProductID == ID);



            if (productToUpdate != null)
            {
                productToUpdate.ProductName = product.ProductName;
                productToUpdate.Description = product.Description;
                productToUpdate.Price = product.Price;
                productToUpdate.Discount = product.Discount;
                productToUpdate.Stock = product.Stock;
                productToUpdate.CreatedAt = product.CreatedAt;

         

                productToUpdate.CategoryID = product.CategoryID;


                if (product.ProductImages != null)
                {

                    foreach (var productImage in productToUpdate.ProductImages)
                    {
                        var oldFileName = Path.GetFileName(new Uri(productImage.ImageUrl).LocalPath);
                        var oldFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", oldFileName);



                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }

                    }


                    foreach (var productImage in product.ProductImages)
                    {
                        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(productImage.ImageFile.FileName)}";
                        var localfilepath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", fileName);

                        using var stream = new FileStream(localfilepath, FileMode.Create);

                        await productImage.ImageFile.CopyToAsync(stream);
                        productImage.ImageUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{fileName}";
                    }


                    productToUpdate.ProductImages = product.ProductImages;

                }

                // when it passed the productsizes [] i want to update ProductSizes data and assign it to [] and it removed from db

                productToUpdate.ProductSizes= product.ProductSizes;


          







                await dbContext.SaveChangesAsync();
                return productToUpdate;
            }
            return null;
        }

        public async Task<ProductCatalog?> DeleteAsync(int ID)
        {
            var productToDelete  = await dbContext.ProductCatalog.Include(pc => pc.Category).Include(pc => pc.ProductImages).FirstOrDefaultAsync(x => x.ProductID == ID);

            if (productToDelete != null)
            {

                foreach (var productImage in productToDelete.ProductImages)
                {

                    var oldFileName = Path.GetFileName(new Uri(productImage.ImageUrl).LocalPath);
                    var oldFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", oldFileName);



                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }

                }


                    dbContext.ProductCatalog.Remove(productToDelete);
                await dbContext.SaveChangesAsync();
                return productToDelete;
            }
            return null;
        }






    }
}
