using EcommerceWeb.Api.Data;
using EcommerceWeb.Api.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWeb.Api.Repositories
{
    public class SQLOrdersRepository : IOrdersRepositorycs
    {
        private readonly EcommerceDbContext dbContext;
        public SQLOrdersRepository(EcommerceDbContext dbContext) {
        
            this.dbContext = dbContext;


        }

        public async Task<Orders> CreateAsync(Orders Order)
        {
            dbContext.Orders.Add(Order);
          await  dbContext.SaveChangesAsync();
            return Order;



        }

        public async Task<Orders?> DeleteAsync(int ID)
        {
            throw new NotImplementedException();
        }


        public async Task<List<Orders>> GetAllAsync([FromQuery] string? filterOn = null, [FromQuery] string? filterQuery = null, [FromQuery] string? sortBy = null, [FromQuery] bool? isAscending = true, [FromQuery] int pageNumber = 1, [FromQuery] int pagesize = 1000)
        {

            var orders= await dbContext.Orders.ToListAsync();

            if (string.IsNullOrEmpty(filterOn) == false && string.IsNullOrEmpty(filterQuery) == false)
            {
                if (filterOn == "OrderDate")
                {
                    orders = orders.Where(x => x.OrderDate.ToString().Contains(filterQuery)).ToList();
                }else if (filterOn =="OrderStatus")
                {
                    // contains accept only OrderStatus
                    orders = orders.Where(x => x.OrderStatus.ToString().Contains(filterQuery)).ToList();

                }

            }

            if (string.IsNullOrEmpty(sortBy) == false)
            {
                if (sortBy == "OrderDate")
                {
                    if (isAscending == true)
                    {
                        orders = orders.OrderBy(x => x.OrderDate).ToList();
                    }
                    else
                    {
                        orders = orders.OrderByDescending(x => x.OrderDate).ToList();
                    }
                }
                else if (sortBy == "TotalAmount")
                {
                    if (isAscending == true)
                    {
                        orders = orders.OrderBy(x => x.TotalAmount).ToList();
                    }
                    else
                    {
                        orders = orders.OrderByDescending(x => x.TotalAmount).ToList();
                    }
                }

            }

            var SkipResults = (pageNumber - 1) * pagesize;







           return orders.Skip(SkipResults).Take(pagesize).ToList();
        }


        public async Task<Orders?> GetByIdAsync(int id)
        {

            return await dbContext.Orders.FindAsync(id);
        }

        public async Task<Orders?> UpdateAsync(int ID, Orders Order)
        {
            throw new NotImplementedException();
        }





    }
}
