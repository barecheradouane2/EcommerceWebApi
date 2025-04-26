using EcommerceWeb.Api.Data;
using EcommerceWeb.Api.Models.Domain;
using EcommerceWeb.Api.Models.DTO;
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
            decimal TotalPrice = 0;

            

            var shippininfo = dbContext.ShippingInfo.FirstOrDefault(s => s.ShippingID == Order.ShippingID);



          //  var productcataog=dbContext.ProductCatalog.FirstOrDefault(p => p.ProductID == Order.OrderItems.FirstOrDefault().ProductID).FirstOrDefault(s => s.Size == "M")


            foreach (var item in Order.OrderItems)
            {
                //var product = dbContext.ProductCatalog.FirstOrDefault(p => p.ProductID == item.ProductID


                var product = await dbContext.ProductCatalog
                                                .Include(pc => pc.ProductSizes).ThenInclude(ps => ps.ProductColorVariant)
                            .FirstOrDefaultAsync(x => x.ProductID == item.ProductID);

                if (product == null)
                {
                    return null;
                }

                if(product.Stock < item.Quantity)
                {
                    throw new Exception("Not enough stock available for product: " + product.ProductName);
                }
                else
                {
                    product.Stock -= item.Quantity;

                }

                if (product.ProductSizes.Count > 0)
                {

                    var productsize = product.ProductSizes?.FirstOrDefault(s => s.Size == item.Size);

                    if (productsize != null && productsize.Quantity >= item.Quantity)
                    {
                        //handle where the qunatiy of other higher then the quntity of product 
                        productsize.Quantity -= item.Quantity;


                    }
                    else
                    {
                        throw new Exception("Not enough stock available for size: " + item.Size);
                    }

                    if(productsize.ProductColorVariant.Count > 0)
                    {

                        var productcolor = product.ProductSizes?.FirstOrDefault(s => s.Size == item.Size)?.ProductColorVariant
                            .FirstOrDefault(c => c.Color == item.Color);


                        if (productcolor != null && item.Color != string.Empty && productcolor.Quantity >= item.Quantity)
                        {

                            productcolor.Quantity -= item.Quantity;


                        }
                        else
                        {
                            throw new Exception("Not enough stock available for color: " + item.Color);
                        }



                    }

                }

                
                item.Price = product.Price - product.Discount;
                item.TotalItemsPrice = item.Price * item.Quantity;

                TotalPrice += item.TotalItemsPrice;
            }
            //something wrong happens here when shippingid is not 0

            if (Order.ShippingStatus == 0)
            {
                TotalPrice += shippininfo.HomeDeliveryPrice;

            }
            else
            {
                TotalPrice += shippininfo.OfficeDeliveryPrice;
            }
            Order.TotalPrice = TotalPrice ;

            Order.Wilaya = dbContext.Wilaya.FirstOrDefault(w => w.WilayaID == Order.WilayaID);
            Order.Commune = dbContext.Commune.FirstOrDefault(c => c.CommuneID == Order.CommuneID);

            Order.ShippingInfo = shippininfo;


            dbContext.Orders.Add(Order);
            await dbContext.SaveChangesAsync();

            return Order;


            // false flase 




        //    var orderdetails  = await dbContext.Orders
        //        .Include(P => P.ShippingInfo).Include(o => o.OrderItems).ThenInclude(oi => oi.ProductCatalog).FirstOrDefaultAsync(o => o.OrderID == Order.OrderID);


        //    orderdetails.TotalPrice = orderdetails.OrderItems.Sum(oi => oi.TotalItemsPrice);

        //    if (orderdetails.ShippingStatus == 0)
        //    {
        //        orderdetails.TotalPrice += orderdetails.ShippingInfo.HomeDeliveryPrice;
        //    }
        //    else
        //    {
        //        orderdetails.TotalPrice += orderdetails.ShippingInfo.OfficeDeliveryPrice;


        //    }

        //orderdetails.OrderItems = orderdetails.OrderItems.Select(oi => new OrderItems
        //{
        //    OrderItemsID = oi.OrderItemsID,
        //    OrderID = oi.OrderID,
        //    ProductID = oi.ProductID,
           
        //    Quantity = oi.Quantity,
        //    ProductCatalog = oi.ProductCatalog,
        //    TotalItemsPrice = oi.ProductCatalog.Price * oi.Quantity,
        //    Price = oi.ProductCatalog.Price
        //}).ToList();


        //    dbContext.Orders.Add(orderdetails);
        //    await dbContext.SaveChangesAsync();

        //    return orderdetails;



        }

        public async Task<Orders?> DeleteAsync(int ID)
        {
            var order = await dbContext.Orders.Include(w => w.Wilaya).Include(c => c.Commune)
                .Include(P => P.ShippingInfo).Include(o => o.OrderItems).ThenInclude(oi => oi.ProductCatalog).FirstOrDefaultAsync(o => o.OrderID == ID);



            //  var order = await dbContext.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.OrderID == ID);
            if (order != null)
            {
                dbContext.Orders.Remove(order);
                await dbContext.SaveChangesAsync();
                return order;
            }
            return null;

        }


        public async Task<PagedResult<Orders>> GetAllAsync([FromQuery] string? filterOn = null, [FromQuery] string? filterQuery = null, [FromQuery] string? sortBy = null, [FromQuery] bool? isAscending = true, [FromQuery] int pageNumber = 1, [FromQuery] int pagesize = 9)
        {

            var orders = await dbContext.Orders.Include(w => w.Wilaya).Include(c => c.Commune).Include(o => o.OrderItems)
    .ThenInclude(oi => oi.ProductCatalog).Include(P => P.ShippingInfo) // Include related products
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
             //           orders = orders.OrderBy(x => x.TotalAmount).ToList();
                    }
                    else
                    {
             //           orders = orders.OrderByDescending(x => x.TotalAmount).ToList();
                    }
                }

            }

            var SkipResults = (pageNumber - 1) * pagesize;


            int totalCount = orders.Count();
            int totalPages = (int)Math.Ceiling(totalCount / (double)pagesize);

            var pagedItems =  orders.Skip(SkipResults).Take(pagesize).ToList();

   
            return new PagedResult<Orders>
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                PageNumber = pageNumber,
                PageSize = pagesize,
                Data = pagedItems
            };
        }


        public async Task<Orders?> GetByIdAsync(int id)
        {


            return await dbContext.Orders.Include(w=>w.Wilaya).Include(c=> c.Commune)
                .Include(P => P.ShippingInfo).Include(o => o.OrderItems).ThenInclude(oi => oi.ProductCatalog).FirstOrDefaultAsync(o => o.OrderID == id);


        }

        public async Task<Orders?> UpdateAsync(int ID, Orders Order)
        {
            var order = await dbContext.Orders.Include(w => w.Wilaya).Include(c => c.Commune).Include(o=>o.ShippingInfo)
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
                //order.DiscountCodeID = Order.DiscountCodeID;
                order.ShippingID = Order.ShippingID;
                order.ShippingStatus = Order.ShippingStatus;

                decimal TotalPrice = 0;

                foreach (var item in Order.OrderItems)
                {

                    var product = await dbContext.ProductCatalog
                                                .Include(pc => pc.ProductSizes).ThenInclude(ps => ps.ProductColorVariant)
                            .FirstOrDefaultAsync(x => x.ProductID == item.ProductID);

                    if (product == null)
                    {
                        return null;
                    }

                    if (product.Stock < item.Quantity)
                    {
                        throw new Exception("Not enough stock available for product: " + product.ProductName);
                    }
                    else
                    {
                        product.Stock -= item.Quantity;

                    }

                    if (product.ProductSizes.Count > 0)
                    {

                        var productsize = product.ProductSizes?.FirstOrDefault(s => s.Size == item.Size);

                        if (productsize != null && productsize.Quantity >= item.Quantity)
                        {
                            //handle where the qunatiy of other higher then the quntity of product 
                            productsize.Quantity -= item.Quantity;


                        }
                        else
                        {
                            throw new Exception("Not enough stock available for size: " + item.Size);
                        }

                        if (productsize.ProductColorVariant.Count > 0)
                        {

                            var productcolor = product.ProductSizes?.FirstOrDefault(s => s.Size == item.Size)?.ProductColorVariant
                                .FirstOrDefault(c => c.Color == item.Color);


                            if (productcolor != null && item.Color != string.Empty && productcolor.Quantity >= item.Quantity)
                            {

                                productcolor.Quantity -= item.Quantity;


                            }
                            else
                            {
                                throw new Exception("Not enough stock available for color: " + item.Color);
                            }



                        }

                    }


                    item.Price = product.Price - product.Discount;
                    item.TotalItemsPrice = item.Price * item.Quantity;

                    item.ProductCatalog= product;
                }
                if (order.ShippingStatus==0)
                {
                    TotalPrice += order.ShippingInfo.HomeDeliveryPrice;
                }
                else
                {
                    TotalPrice += order.ShippingInfo.OfficeDeliveryPrice;
                }
                order.TotalPrice = TotalPrice;  

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
