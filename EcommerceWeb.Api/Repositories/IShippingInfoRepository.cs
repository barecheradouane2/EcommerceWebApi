using EcommerceWeb.Api.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWeb.Api.Repositories
{
    public interface IShippingInfoRepository
    {

        Task<List<ShippingInfo>> GetAllAsync([FromQuery] string? filterOn = null, [FromQuery] string? filterQuery = null, [FromQuery] string? sortBy = null, [FromQuery] bool? isAscending = true, [FromQuery] int pageNumber = 1, [FromQuery] int pagesize = 1000);

        Task<ShippingInfo?> GetByIdAsync(int id);

        Task<ShippingInfo> CreateAsync(ShippingInfo shippingInfo);

        Task<ShippingInfo?> UpdateAsync(int ID, ShippingInfo shippingInfo);

        Task<ShippingInfo?> DeleteAsync(int ID);

        Task <List<Commune> > GetCommunOfWilaya (int WilayaID);

        Task <List<Wilaya>> GetWilayaList ();


    }
}
