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

        public async Task<IActionResult> GetAllAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pagesize = 1000)
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
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderAsync(int id)
        {
            var order = await ordersRepository.GetByIdAsync(id);
            var orderItems = await orderItemsRepository.GetByOrderIDAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            var OrdersDto = new OrdersDto
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

                OrderItems = orderItems.Select(x => new OrderItemsDTO
                {
                    OrderItemsID = x.OrderItemsID,
                    OrderID = x.OrderID,
                    ProductID = x.ProductID,
                    Quantity = x.Quantity,
                    Price = x.Price,
                    TotalItemsPrice = x.TotalItemsPrice,
                    ProductName = x.ProductCatalog?.ProductName // Access ProductName directly
                }).ToList(),



                DiscountCodeID = order.DiscountCodeID,
                ShippingID = order.ShippingID,
                ShippingStatus = order.ShippingStatus
            };
            return Ok(OrdersDto);
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
                ShippingStatus = addOrderRequestDTO.ShippingStatus


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
                ShippingStatus = newOrder.ShippingStatus
            };


              if (newOrder == null)
               {
                   return NotFound(new { Message = $"Order with ID {newOrder.OrderID} not found." });
               }

              var orderItems = new List<OrderItemsDTO>();

            foreach (var item in addOrderRequestDTO.OrderItems)
               {
                // get the product information 
             
                       var product = await productRepository.GetByIdAsync(item.ProductID);

                if (product == null)
                {
                    return BadRequest(new { Message = $"Product with ID {item.ProductID} not found." });
                }



                var OrderItemsDomainModel = new OrderItems
                   {
                       OrderID = newOrder.OrderID,
                       ProductID = item.ProductID,
                       Quantity = item.Quantity,
                       Price = product.Price - product.Discount,
                       TotalItemsPrice = (product.Price - product.Discount) * item.Quantity
                };


               var  neworderItems= await orderItemsRepository.CreateAsync(OrderItemsDomainModel);

                orderItems.Add(new OrderItemsDTO
                {
                    OrderItemsID = neworderItems.OrderItemsID,
                    OrderID = neworderItems.OrderID,
                    ProductID = neworderItems.ProductID,
                    Quantity = neworderItems.Quantity,
                    Price = neworderItems.Price,
                    ProductName= product.ProductName,
                    TotalItemsPrice = neworderItems.TotalItemsPrice
                });


            }

            OrdersDto.OrderItems = orderItems;

            /* return CreatedAtAction(nameof(GetOrderAsync), new { id = newOrder.OrderID }, newOrder); */


            // return Created($"/api/Orders/{OrdersDto.OrderID}", OrdersDto);

        



            return Created($"/api/Orders/{OrdersDto.OrderID}", OrdersDto);


        }




    }
    }
