using EcommerceWeb.Api.Data;
using EcommerceWeb.Api.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWeb.Api.Repositories
{
    public class SQLProductImageRepository : IProductImageRepository

    {
        private readonly EcommerceDbContext dbContext;

        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;


        public SQLProductImageRepository(EcommerceDbContext dbContext,IWebHostEnvironment   webHostEnvironment,IHttpContextAccessor httpContextAccessor)
        {
            this.dbContext = dbContext;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ProductImages> CreateAsync(ProductImages productImage)
        {
            //lazem extention 
            var localfilepath =Path.Combine(webHostEnvironment.ContentRootPath,"Images", $"{Guid.NewGuid().ToString()}{Path.GetExtension(productImage.ImageFile.FileName)}");


            //uploda image to path

            using var stream = new FileStream(localfilepath, FileMode.Create);

            await productImage.ImageFile.CopyToAsync(stream);

            var urlfilepath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{productImage.ImageFile}{Path.GetExtension(productImage.ImageFile.FileName)}";


           productImage.ImageUrl= urlfilepath;




            await dbContext.ProductImages.AddAsync(productImage);
            await dbContext.SaveChangesAsync();
            return productImage;
        }


        public async Task<ProductImages?> UpdateAsync(int ID, ProductImages productImage)
        {
             var productImageToUpdate = await dbContext.ProductImages.FirstOrDefaultAsync(pi => pi.ProductID == ID && pi.ImageOrder == productImage.ImageOrder);

            if (productImageToUpdate == null)
            {
                return null;
            }

            var localfilepath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{Guid.NewGuid().ToString()}{Path.GetExtension(productImage.ImageFile.FileName)}");


            //uploda image to path

            using var stream = new FileStream(localfilepath, FileMode.Create);

            await productImage.ImageFile.CopyToAsync(stream);

            var urlfilepath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{productImage.ImageFile}{Path.GetExtension(productImage.ImageFile.FileName)}";

            productImageToUpdate.ImageOrder = productImage.ImageOrder;
            productImageToUpdate.ImageUrl = urlfilepath;
            productImageToUpdate.ProductID = productImage.ProductID;

            await dbContext.SaveChangesAsync();
            return productImageToUpdate;
        }

        public async Task<List<ProductImages?>> GetByIdAsync(int ID)
        {

            return await dbContext.ProductImages.Where(pi => pi.ProductID == ID).ToListAsync();
        }

        public async Task<ProductImages?> DeleteAsync(int ID, int ImageOrder)
        {
            var productImageToDelete = await dbContext.ProductImages.FirstOrDefaultAsync(pi => pi.ProductID == ID && pi.ImageOrder== ImageOrder);

            if (productImageToDelete == null)
            {
                return null;
            }

            dbContext.ProductImages.Remove(productImageToDelete);
            await dbContext.SaveChangesAsync();
            return productImageToDelete;
        }


        }
    
}
