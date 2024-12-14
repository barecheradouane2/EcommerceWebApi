using EcommerceWeb.Api.Models.Domain;

namespace EcommerceWeb.Api.Repositories
{
    public interface ICategoryRepository
    {


        Task<List<Category>> GetAllAsync();

        Task<Category?> GetByIdAsync(int id);

        Task<Category> CreateAsync(Category product);

        Task<Category?> UpdateAsync(int ID, Category product);

        Task<Category?> DeleteAsync(int ID);




    }
}
