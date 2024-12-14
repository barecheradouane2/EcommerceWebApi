using EcommerceWeb.Api.Data;
using EcommerceWeb.Api.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWeb.Api.Repositories
{
    public class SQLCategoryRepository : ICategoryRepository
    {
        private readonly EcommerceDbContext dbContext;
        public SQLCategoryRepository(EcommerceDbContext dbContext) {

            this.dbContext = dbContext;

        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await dbContext.Category.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await dbContext.Category.FindAsync(id);
        }


        public async Task<Category> CreateAsync(Category Category)
        {
            await dbContext.Category.AddAsync(Category);
            await dbContext.SaveChangesAsync();
            return Category;
        }

        public async Task<Category?> UpdateAsync(int ID, Category Category)
        {
            var CategoryToUpdate = await dbContext.Category.FindAsync(ID);
            if (CategoryToUpdate != null)
            {
                CategoryToUpdate.CategoryName = Category.CategoryName;
                CategoryToUpdate.Description = Category.Description;
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
                dbContext.Category.Remove(CategoryToDelete);
                await dbContext.SaveChangesAsync();
                return CategoryToDelete;
            }
            return null;
        }

      
    }
}
