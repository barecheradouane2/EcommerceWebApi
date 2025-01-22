using EcommerceWeb.Api.Data;
using EcommerceWeb.Api.Models.Domain;
using EcommerceWeb.Api.Models.DTO;
using EcommerceWeb.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly EcommerceDbContext dbContext;

        private readonly IOrdersRepositorycs ordersRepository;

        private readonly IProductRepository productRepository;

        private readonly IOrdersItems orderItemsRepository;



        public OrdersController(EcommerceDbContext dbContext, IOrdersItems orderItemsRepository, IOrdersRepositorycs ordersRepositorycs, IProductRepository productRepository)
        {
            this.orderItemsRepository = orderItemsRepository;
            this.ordersRepository = ordersRepositorycs;
            this.dbContext = dbContext;
            this.productRepository = productRepository;
        }


        [HttpGet]

        /*   public async Task<IActionResult> GetAllAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pagesize = 1000)
           {
               // get 
               var orders = await ordersRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pagesize);

               var ordersDTO= new List<OrdersDto>();

               foreach (var order in orders)
               {
                   //   var ordersDTOitems = new List<OrderItemsDTO>();

                   var OrderItems = await orderItemsRepository.GetByOrderIDAsync(order.OrderID);
                   var OrderItemsDTOlist = new List<OrderItemsDTO>();

                   foreach (var item in OrderItems)
                   {
                       var product = await productRepository.GetByIdAsync(item.ProductID);
                       OrderItemsDTOlist.Add(new OrderItemsDTO
                       {
                           OrderItemsID = item.OrderItemsID,
                           OrderID = item.OrderID,
                           ProductID = item.ProductID,
                           Quantity = item.Quantity,
                           Price = item.Price,
                           TotalItemsPrice = item.TotalItemsPrice,
                           ProductName = product?.ProductName // Access ProductName directly
                       });
                   }


                   ordersDTO.Add(new OrdersDto
                   {
                       OrderID = order.OrderID,
                       OrderDate = order.OrderDate,
                       TotalAmount = order.TotalAmount,
                       OrderStatus = order.OrderStatus,
                       FullName = order.FullName,
                       TelephoneNumber = order.TelephoneNumber,
                       OrderAddress = order.OrderAddress,
                       Wilaya = order.Wilaya,
                       Commune = order.Commune,
                       OrderItems= OrderItemsDTOlist,
                       DiscountCodeID = order.DiscountCodeID,
                       ShippingID = order.ShippingID,
                       ShippingStatus = order.ShippingStatus
                   });
               }


               // map to dto

               return Ok(ordersDTO);
           }*/


        public async Task<IActionResult> GetAllAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
    [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 50)
        {
            // Validate pageSize
            pageSize = Math.Min(pageSize, 50);

            // Fetch data in one query with filtering, sorting, and pagination
            var orders = await ordersRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);

            // Map orders to DTOs
            var ordersDTO = orders.Select(order => new OrdersDto
            {
                OrderID = order.OrderID,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                OrderStatus = order.OrderStatus,
                FullName = order.FullName,
                TelephoneNumber = order.TelephoneNumber,
                OrderAddress = order.OrderAddress,
                Wilaya = order.Wilaya,
                Commune = order.Commune,
                DiscountCodeID = order.DiscountCodeID,
                ShippingID = order.ShippingID,
                ShippingStatus = order.ShippingStatus,
                OrderItems = order.OrderItems.Select(item => new OrderItemsDTO
                {
                    OrderItemsID = item.OrderItemsID,
                    OrderID = item.OrderID,
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    TotalItemsPrice = item.TotalItemsPrice,
                    ProductName = item.ProductCatalog?.ProductName
                }).ToList()
            }).ToList();

            return Ok(ordersDTO);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderAsync(int id)
        {
            var order = await ordersRepository.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            var ordersDto = new OrdersDto
            {
                OrderID = order.OrderID,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                OrderStatus = order.OrderStatus,
                FullName = order.FullName,
                TelephoneNumber = order.TelephoneNumber,
                OrderAddress = order.OrderAddress,
                Wilaya = order.Wilaya,
                Commune = order.Commune,
                DiscountCodeID = order.DiscountCodeID,
                ShippingID = order.ShippingID,
                ShippingStatus = order.ShippingStatus,
                OrderItems = order.OrderItems.Select(oi => new OrderItemsDTO
                {
                    OrderItemsID = oi.OrderItemsID,

                    ProductID = oi.ProductID,
                    ProductName = oi.ProductCatalog.ProductName,
                    Quantity = oi.Quantity,
                    Price = oi.Price,
                    TotalItemsPrice = oi.TotalItemsPrice
                }).ToList()
            };

            return Ok(ordersDto);
        }


        [HttpPost]

        public async Task<IActionResult> CreateOrderAsync([FromBody] AddOrderRequestDTO addOrderRequestDTO)
        {

            var OrderDomainModel = new Orders
            {
                OrderDate = addOrderRequestDTO.OrderDate,
                TotalAmount = addOrderRequestDTO.TotalAmount,
                OrderStatus = addOrderRequestDTO.OrderStatus,
               
                FullName = addOrderRequestDTO.FullName,
                TelephoneNumber = addOrderRequestDTO.TelephoneNumber,
                OrderAddress = addOrderRequestDTO.OrderAddress,
                Wilaya = addOrderRequestDTO.Wilaya,
                Commune = addOrderRequestDTO.Commune,
               
                DiscountCodeID = addOrderRequestDTO.DiscountCodeID,
                ShippingID = addOrderRequestDTO.ShippingID,
                ShippingStatus = addOrderRequestDTO.ShippingStatus,

                OrderItems = addOrderRequestDTO.OrderItems.Select(item => new OrderItems
                {
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    Price = item.Price,
                   TotalItemsPrice = item.TotalItemsPrice
                }).ToList()


            };

            
            var newOrder = await ordersRepository.CreateAsync(OrderDomainModel);

            var OrdersDto = new OrdersDto
              {

                  OrderID = newOrder.OrderID,
                  OrderDate = newOrder.OrderDate,
                  TotalAmount = newOrder.TotalAmount,
                  OrderStatus = newOrder.OrderStatus,
                  FullName = newOrder.FullName,
                  TelephoneNumber = newOrder.TelephoneNumber,
                  OrderAddress = newOrder.OrderAddress,
                  Wilaya = newOrder.Wilaya,
                  Commune = newOrder.Commune,
                  DiscountCodeID = newOrder.DiscountCodeID,
                  ShippingID = newOrder.ShippingID,
                  ShippingStatus = newOrder.ShippingStatus,

                   OrderItems = newOrder.OrderItems.Select(x => new OrderItemsDTO
                   {
                       OrderItemsID = x.OrderItemsID,
                       OrderID = x.OrderID,
                       ProductID = x.ProductID,
                       Quantity = x.Quantity,
                       Price = x.Price,
                       TotalItemsPrice = x.TotalItemsPrice,
                       ProductName = x.ProductCatalog?.ProductName // Access ProductName directly
                   }).ToList()

                    

            };


                if (newOrder == null)
                 {
                     return NotFound(new { Message = $"Order with ID {newOrder.OrderID} not found." });
                 }


          


            /*

            {
"orderItems": [
{
  "productID":1,
  "quantity": 2
}
],
"orderDate": "2025-01-18T22:48:00.835Z",
"totalAmount": 0,
"orderStatus": 0,
"fullName": "radouane",
"telephoneNumber": "055401369",
"orderAddress": "soumame",
"wilaya": "mila",
"commune": "ferdjioua",
"discountCodeID": 1,
"shippingID": 1,
"shippingStatus": 0
}









             */






            return Created($"/api/Orders/{OrdersDto.OrderID}", OrdersDto);


        }


        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteOrderAsync(int id)
        {
            var order = await ordersRepository.DeleteAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            var orderDto = new OrdersDto
            {
                OrderID = order.OrderID,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                OrderStatus = order.OrderStatus,
                FullName = order.FullName,
                TelephoneNumber = order.TelephoneNumber,
                OrderAddress = order.OrderAddress,
                Wilaya = order.Wilaya,
                Commune = order.Commune,
                DiscountCodeID = order.DiscountCodeID,
                ShippingID = order.ShippingID,
                ShippingStatus = order.ShippingStatus,
                OrderItems = order.OrderItems.Select(oi => new OrderItemsDTO
                {
                    OrderItemsID = oi.OrderItemsID,
                    ProductID = oi.ProductID,
                    ProductName=oi.ProductCatalog.ProductName,
                    Quantity = oi.Quantity,
                    Price = oi.Price,
                    TotalItemsPrice = oi.TotalItemsPrice
                }).ToList()
            };

            return Ok(orderDto);
        }


        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateOrderAsync(int id, [FromBody] UpdateOrderRequestDTO addOrderRequestDTO)
        {
            var order = await ordersRepository.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            var ordertest= new Orders();

            ordertest.OrderDate = addOrderRequestDTO.OrderDate;
            ordertest.TotalAmount = addOrderRequestDTO.TotalAmount;
            ordertest.OrderStatus = addOrderRequestDTO.OrderStatus;
            ordertest.FullName = addOrderRequestDTO.FullName;
            ordertest.TelephoneNumber = addOrderRequestDTO.TelephoneNumber;
            ordertest.OrderAddress = addOrderRequestDTO.OrderAddress;
            ordertest.Wilaya = addOrderRequestDTO.Wilaya;
            ordertest.Commune = addOrderRequestDTO.Commune;
            ordertest.DiscountCodeID = addOrderRequestDTO.DiscountCodeID;
            ordertest.ShippingID = addOrderRequestDTO.ShippingID;
            ordertest.ShippingStatus = addOrderRequestDTO.ShippingStatus;

            // here is the problem men 

            ordertest.OrderItems = addOrderRequestDTO.OrderItems.Select(item => new OrderItems
            {
                OrderItemsID= item.OrderItemsID,
                ProductID = item.ProductID,
                Quantity = item.Quantity,
               
            }).ToList();

            var updatedOrder = await ordersRepository.UpdateAsync(id, ordertest);

            var orderDto = new OrdersDto
            {
                OrderID = updatedOrder.OrderID,
                OrderDate = updatedOrder.OrderDate,
                TotalAmount = updatedOrder.TotalAmount,
                OrderStatus = updatedOrder.OrderStatus,
                FullName = updatedOrder.FullName,
                TelephoneNumber = updatedOrder.TelephoneNumber,
                OrderAddress = updatedOrder.OrderAddress,
                Wilaya = updatedOrder.Wilaya,
                Commune = updatedOrder.Commune,
                DiscountCodeID = updatedOrder.DiscountCodeID,
                ShippingID = updatedOrder.ShippingID,
                ShippingStatus = updatedOrder.ShippingStatus,
                OrderItems = updatedOrder.OrderItems.Select(oi => new OrderItemsDTO
                {
                    OrderItemsID = oi.OrderItemsID,
                    ProductID = oi.ProductID,
                    ProductName = oi.ProductCatalog.ProductName,
                    Quantity = oi.Quantity,
                    Price = oi.ProductCatalog.Price,
                    TotalItemsPrice = oi.Quantity * oi.ProductCatalog.Price
                }).ToList()
            };

            return Ok(orderDto);
        }







    }
    }
