using EcommerceWeb.Api.Models.Domain;

namespace EcommerceWeb.Api.Repositories
{
    public interface IProductImageRepository
    {

        Task<ProductImages> CreateAsync(ProductImages productImage);

        Task<ProductImages?> UpdateAsync(int ID, ProductImages productImage);

        Task<List<ProductImages?>> GetByIdAsync(int id);

        Task<ProductImages?> DeleteAsync(int ID, int ImageOrder);





    }
}
