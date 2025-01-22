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
            decimal totalamount = 0;


              foreach (var item in Order.OrderItems)
               {
                   var product = dbContext.ProductCatalog.FirstOrDefault(p => p.ProductID == item.ProductID);
                   if (product == null)
                   {
                       return null;
                   }
                   item.Price = product.Price;
                   item.TotalItemsPrice = item.Price * item.Quantity;

                totalamount += item.Quantity;
            }


            Order.TotalAmount=totalamount;


            dbContext.Orders.Add(Order);
          await  dbContext.SaveChangesAsync();
            return Order;



        }

        public async Task<Orders?> DeleteAsync(int ID)
        {
            var order = await dbContext.Orders
    .Include(o => o.OrderItems)
        .ThenInclude(oi => oi.ProductCatalog)
    .FirstOrDefaultAsync(o => o.OrderID == ID);


          //  var order = await dbContext.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.OrderID == ID);
            if (order != null)
            {
                dbContext.Orders.Remove(order);
                await dbContext.SaveChangesAsync();
                return order;
            }
            return null;

        }


        public async Task<List<Orders>> GetAllAsync([FromQuery] string? filterOn = null, [FromQuery] string? filterQuery = null, [FromQuery] string? sortBy = null, [FromQuery] bool? isAscending = true, [FromQuery] int pageNumber = 1, [FromQuery] int pagesize = 1000)
        {

            var orders = await dbContext.Orders.Include(o => o.OrderItems)
    .ThenInclude(oi => oi.ProductCatalog) // Include related products
    .ToListAsync();












            //  var orders = await dbContext.Orders.ToListAsync();

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


            return await dbContext.Orders
         .Include(o => o.OrderItems).ThenInclude(oi => oi.ProductCatalog).FirstOrDefaultAsync(o => o.OrderID == id);

        }

        public async Task<Orders?> UpdateAsync(int ID, Orders Order)
        {
            var order = await dbContext.Orders
         .Include(o => o.OrderItems).ThenInclude(oi => oi.ProductCatalog).FirstOrDefaultAsync(o => o.OrderID == ID);

         


            if (order != null)
            {

                order.OrderDate = Order.OrderDate;
                order.OrderStatus = Order.OrderStatus;
                order.FullName = Order.FullName;
                order.TelephoneNumber = Order.TelephoneNumber;
                order.Wilaya = Order.Wilaya;
                order.Commune = Order.Commune;
                order.OrderAddress = Order.OrderAddress;
                order.DiscountCodeID = Order.DiscountCodeID;
                order.ShippingID = Order.ShippingID;
                order.ShippingStatus = Order.ShippingStatus;

                

                foreach (var item in Order.OrderItems)
                {
                    var product = dbContext.ProductCatalog.FirstOrDefault(p => p.ProductID == item.ProductID);
                    if (product == null)
                    {
                        return null;
                    }
                    item.Price = product.Price;
                    item.TotalItemsPrice = item.Price * item.Quantity;

                    item.ProductCatalog= product;
                }

                order.OrderItems = Order.OrderItems;






                // Mark the order entity as updated
                // dbContext.Orders.Update(order);



                // Save changes
                await dbContext.SaveChangesAsync();

                return order;
            }

            return null;
        }



        /*
         
         {
  "orderItems": [
    {
      "productID":1,
      "quantity": 3
    }
  ],
  "orderDate": "2025-01-19T14:13:23.254Z",
  "totalAmount": 0,
  "orderStatus": 1,
  "fullName": "radouane",
  "telephoneNumber": "0657113994",
  "orderAddress": "soumame",
  "wilaya": "mila",
  "commune": "ferdjioua",
  "discountCodeID": 1,
  "shippingID": 1,
  "shippingStatus": 0
}
         
         
   
         
         */





    }
}
