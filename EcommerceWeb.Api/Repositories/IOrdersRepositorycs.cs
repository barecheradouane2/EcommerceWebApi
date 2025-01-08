using EcommerceWeb.Api.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWeb.Api.Repositories
{
    public interface IOrdersRepositorycs
    {

        Task<List<Orders>> GetAllAsync([FromQuery] string? filterOn = null, [FromQuery] string? filterQuery = null, [FromQuery] string? sortBy = null, [FromQuery] bool? isAscending = true, [FromQuery] int pageNumber = 1, [FromQuery] int pagesize = 1000);

        Task<Orders?> GetByIdAsync(int id);

        Task<Orders> CreateAsync(Orders Order);

        Task<Orders?> UpdateAsync(int ID, Orders Order);

        Task<Orders?> DeleteAsync(int ID);
    }
}
