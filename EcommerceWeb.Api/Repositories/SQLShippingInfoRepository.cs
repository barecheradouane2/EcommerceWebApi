using EcommerceWeb.Api.Data;
using EcommerceWeb.Api.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWeb.Api.Repositories
{
    public class SQLShippingInfoRepository : IShippingInfoRepository
    {

        private readonly EcommerceDbContext dbContext;

        public SQLShippingInfoRepository(EcommerceDbContext dbContext)
        {
            this.dbContext = dbContext;
            
        }


        public  async Task<ShippingInfo> CreateAsync(ShippingInfo shippingInfo)
        {
           
            await dbContext.ShippingInfo.AddAsync(shippingInfo);

            await dbContext.SaveChangesAsync();

            return shippingInfo;


        }

        public async Task<ShippingInfo?> DeleteAsync(int ID)
        {
          
            var shippininfoDelete=await dbContext.ShippingInfo.FirstOrDefaultAsync(x=>x.ShippingID==ID);

            if (shippininfoDelete!=null)
            {
                dbContext.ShippingInfo.Remove(shippininfoDelete);
                await dbContext.SaveChangesAsync();
                return shippininfoDelete;
            }
            return null;
        }

        public async Task<List<ShippingInfo>> GetAllAsync([FromQuery] string? filterOn = null, [FromQuery] string? filterQuery = null, [FromQuery] string? sortBy = null, [FromQuery] bool? isAscending = true, [FromQuery] int pageNumber = 1, [FromQuery] int pagesize = 1000)
        {

            var shipping = dbContext.ShippingInfo.AsQueryable();

            if (string.IsNullOrEmpty(filterOn) == false && string.IsNullOrEmpty(filterQuery) == false)
            {
                if (filterOn == "WilayaFrom")
                {
                    //shipping = shipping.Where(x => x.WilayaFrom.Contains(filterQuery));
                }
                 if (filterOn == "WilayaTo")
                {
                    shipping = shipping.Where(x => x.WilayaTo.Contains(filterQuery));
                }

            }


            if (string.IsNullOrEmpty(sortBy) == false)
            {

                if (sortBy == "WilayaFrom")
                {
                    if (isAscending == true)
                    {
                        //shipping = shipping.OrderBy(x => x.WilayaFrom);
                    }
                    else
                    {
                        //shipping = shipping.OrderByDescending(x => x.WilayaFrom);
                    }

                }
            }


            var SkipResults = (pageNumber - 1) * pagesize;


            return await shipping.Skip(SkipResults).Take(pagesize).ToListAsync();



        }

        public async Task<ShippingInfo?> GetByIdAsync(int id)
        {
            return await dbContext.ShippingInfo.FirstOrDefaultAsync(x => x.ShippingID == id);
        }

        public async Task<ShippingInfo?> UpdateAsync(int ID, ShippingInfo shippingInfo)
        {

            var ShipingToUpdate = await dbContext.ShippingInfo.FirstOrDefaultAsync(x => x.ShippingID == ID);

            if (ShipingToUpdate!=null)
            {
                //ShipingToUpdate.WilayaFrom = shippingInfo.WilayaFrom;
                //ShipingToUpdate.WilayaTo = shippingInfo.WilayaTo;
                ShipingToUpdate.OfficeDeliveryPrice = shippingInfo.OfficeDeliveryPrice;
                ShipingToUpdate.HomeDeliveryPrice= shippingInfo.HomeDeliveryPrice;

                ShipingToUpdate.ShipingStatus= shippingInfo.ShipingStatus;

                await dbContext.SaveChangesAsync();

                return ShipingToUpdate;

            }

            return null;


        }


        public async Task<List<Commune>> GetCommunOfWilaya(int WilayaID)
        {
            var communes = await dbContext.Commune
                .Where(x => x.WilayaID == WilayaID)
                .ToListAsync();

            return communes;
        }








    }
}
