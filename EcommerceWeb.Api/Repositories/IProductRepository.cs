using EcommerceWeb.Api.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWeb.Api.Repositories
{
    public interface IProductRepository 
    {

      Task <List<ProductCatalog>> GetAllAsync([FromQuery] string? filterOn=null, [FromQuery] string? filterQuery=null, [FromQuery] string? sortBy = null, [FromQuery] bool? isAscending=true, [FromQuery] int pageNumber = 1, [FromQuery] int pagesize = 1000);

        Task <ProductCatalog?> GetByIdAsync(int id);

        Task<ProductCatalog> CreateAsync(ProductCatalog product);

        Task<ProductCatalog?> UpdateAsync(int ID,ProductCatalog product);

        Task<ProductCatalog?> DeleteAsync(int ID);


    }
}
