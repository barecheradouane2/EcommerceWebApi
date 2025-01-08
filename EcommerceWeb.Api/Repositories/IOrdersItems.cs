using EcommerceWeb.Api.Models.Domain;

namespace EcommerceWeb.Api.Repositories
{
    public interface IOrdersItems 
    {


        Task<List<OrderItems>> GetAllAsync();

        Task<OrderItems?> GetByIdAsync(int id);

        Task<List<OrderItems>> GetByOrderIDAsync(int OrderID);

        Task<OrderItems> CreateAsync(OrderItems OrderItems);

        Task<OrderItems?> UpdateAsync(int ID, OrderItems OrderItems);

        Task<OrderItems?> DeleteAsync(int ID);





    }
}
