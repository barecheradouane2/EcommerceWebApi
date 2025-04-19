using EcommerceWeb.Api.Data;
using EcommerceWeb.Api.Models.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using EcommerceWeb.Api.Migrations;

namespace EcommerceWeb.Api.Repositories
{
    public class SQLCategoryRepository : ICategoryRepository
    {
        private readonly EcommerceDbContext dbContext;

        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        public SQLCategoryRepository(EcommerceDbContext dbContext, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor) {

            this.dbContext = dbContext;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;

        }

        public async Task<List<Category>> GetAllAsync()

        {
            var categories = await dbContext.Category.ToListAsync();
         return categories;
          
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await dbContext.Category.Include(p=>p.ProductCatalog).ThenInclude(s=>s.ProductImages).FirstOrDefaultAsync(c => c.CategoryID == id);




        }


        public async Task<Category> CreateAsync(Category Category)
        {

            if(Category.ImageFile != null)
            {

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(Category.ImageFile.FileName)}";
                var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", fileName);

                // Save the image
                using var stream = new FileStream(localFilePath, FileMode.Create);
                await Category.ImageFile.CopyToAsync(stream);

                // Generate the accessible URL
                Category.ImagePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{fileName}";


            }



  

            // Save category to DB
            await dbContext.Category.AddAsync(Category);
            await dbContext.SaveChangesAsync();

            return Category;
        }

        public async Task<Category?> UpdateAsync(int ID, Category Category)
        {
            var CategoryToUpdate = await dbContext.Category.FirstOrDefaultAsync(c => c.CategoryID == ID);
            if (CategoryToUpdate != null)
            {
                CategoryToUpdate.CategoryName = Category.CategoryName;
                CategoryToUpdate.Description = Category.Description;

                CategoryToUpdate.UpdatedAt = DateTime.UtcNow;

                if (Category.ImageFile !=null)
                {

                    var oldFileName = Path.GetFileName(new Uri(CategoryToUpdate.ImagePath).LocalPath);
                    var oldFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", oldFileName);

                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }


                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(Category.ImageFile.FileName)}";
                    var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", fileName);

                    // Save the image
                    using var stream = new FileStream(localFilePath, FileMode.Create);
                    await Category.ImageFile.CopyToAsync(stream);

                    // Generate the accessible URL
                    CategoryToUpdate.ImagePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{fileName}";

                }








                await dbContext.SaveChangesAsync();
                return CategoryToUpdate;
            }
            return null;
        }


        public async Task<Category?> DeleteAsync(int ID)
        {
            var CategoryToDelete = await dbContext.Category.FindAsync(ID);
            if (CategoryToDelete != null)
            {

                if (CategoryToDelete.ImagePath!=string.Empty)
                {

                    var oldFileName = Path.GetFileName(new Uri(CategoryToDelete.ImagePath).LocalPath);
                    var oldFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", oldFileName);

                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }








                }





                dbContext.Category.Remove(CategoryToDelete);
                await dbContext.SaveChangesAsync();
                return CategoryToDelete;
            }
            return null;
        }

      
    }
}
