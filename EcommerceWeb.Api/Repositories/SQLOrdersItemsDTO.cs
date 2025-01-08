using EcommerceWeb.Api.Data;
using EcommerceWeb.Api.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWeb.Api.Repositories
{
    public class SQLOrdersItemsDTO : IOrdersItems
    {

        private readonly EcommerceDbContext dbContext;
        public SQLOrdersItemsDTO(EcommerceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<List<OrderItems>> GetAllAsync()
        {
            return dbContext.OrderItems.ToListAsync();
        }


        public Task<OrderItems?> GetByIdAsync(int id)
        {
            return dbContext.OrderItems.FirstOrDefaultAsync(x => x.OrderItemsID == id);
        }


        public async Task<OrderItems> CreateAsync(OrderItems OrderItems)
        {
            dbContext.OrderItems.Add(OrderItems);
            await dbContext.SaveChangesAsync();
            return OrderItems;
        }


        public async Task<OrderItems?> UpdateAsync(int ID, OrderItems OrderItems)
        {
            var existingOrderItems = await dbContext.OrderItems.FirstOrDefaultAsync(x => x.OrderItemsID == ID);
            if (existingOrderItems != null)
            {
                existingOrderItems.OrderID = OrderItems.OrderID;
                existingOrderItems.ProductID = OrderItems.ProductID;
                existingOrderItems.Quantity = OrderItems.Quantity;
                existingOrderItems.Price = OrderItems.Price;
                existingOrderItems.TotalItemsPrice = OrderItems.TotalItemsPrice;
                await dbContext.SaveChangesAsync();
                return existingOrderItems;
            }
            return null;
        }


        public async Task<OrderItems?> DeleteAsync(int ID)
        {
            var existingOrderItems = await dbContext.OrderItems.FirstOrDefaultAsync(x => x.OrderItemsID == ID);
            if (existingOrderItems != null)
            {
                dbContext.OrderItems.Remove(existingOrderItems);
                await dbContext.SaveChangesAsync();
                return existingOrderItems;
            }
            return null;
        }

      
            public Task<List<OrderItems>> GetByOrderIDAsync(int orderID)
            {
                return dbContext.OrderItems
                    .Include(x => x.ProductCatalog) // Assuming ProductCatalog is the navigation property for Product
                    .Where(x => x.OrderID == orderID)
                    .ToListAsync();
            }


        




    }
}
