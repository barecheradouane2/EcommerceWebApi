using AutoMapper;
using EcommerceWeb.Api.Data;
using EcommerceWeb.Api.Models.Domain;
using EcommerceWeb.Api.Models.DTO;
using EcommerceWeb.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
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

        private readonly IMapper mapper;



        public OrdersController(EcommerceDbContext dbContext, IOrdersItems orderItemsRepository, IOrdersRepositorycs ordersRepositorycs, IProductRepository productRepository, IMapper mapper)
        {
            this.orderItemsRepository = orderItemsRepository;
            this.ordersRepository = ordersRepositorycs;
            this.dbContext = dbContext;
            this.productRepository = productRepository;
            this.mapper = mapper;
        }


        [HttpGet]

      


        public async Task<IActionResult> GetAllAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
    [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10)
        {
            // Validate pageSize
            pageSize = Math.Min(pageSize, 50);

            // Fetch data in one query with filtering, sorting, and pagination
            var orders = await ordersRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);

            // Map orders to DTOs
            var ordersDTO = orders.Data.Select(order => new OrdersDto
            {
                OrderID = order.OrderID,
                OrderDate = order.OrderDate,
            
                OrderStatus = order.OrderStatus,
                FullName = order.FullName,
                TelephoneNumber = order.TelephoneNumber,
                OrderAddress = order.OrderAddress,
                WilayaID = order.WilayaID,
                CommuneID = order.CommuneID,
                Wilaya = order.Wilaya,
                Commune = order.Commune,

              
                ShippingID = order.ShippingID,
                ShippingStatus = order.ShippingStatus,
                ShippingInfo = new ShippingInfoDTO
                {
                    ShippingID = order.ShippingInfo.ShippingID,
                    WilayaID = order.ShippingInfo.WilayaID,
                    ShipingStatus = order.ShippingInfo.ShipingStatus,

                    

                    HomeDeliveryPrice = order.ShippingInfo.HomeDeliveryPrice,
                    OfficeDeliveryPrice = order.ShippingInfo.OfficeDeliveryPrice


                },
                OrderItems = order.OrderItems.Select(item => new OrderItemsDTO
                {
                    OrderItemsID = item.OrderItemsID,
                    OrderID = item.OrderID,
                    Color = item.Color,
                    Size = item.Size,
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    TotalItemsPrice = item.TotalItemsPrice,
                    ProductName = item.ProductCatalog?.ProductName
                }).ToList()
            }).ToList();


            var pagedResultDTO = new PagedResult<OrdersDto>
            {
                TotalCount = orders.TotalCount,
                TotalPages = orders.TotalPages,
                PageNumber = orders.PageNumber,
                PageSize = orders.PageSize,
                Data = ordersDTO
            };





            return Ok(pagedResultDTO);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderAsync(int id)
        {
            var order = await ordersRepository.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            //var ordersDto = mapper.Map<OrdersDto>(order);

            var ordersDto = new OrdersDto
            {
                OrderID = order.OrderID,
                OrderDate = order.OrderDate,

                OrderStatus = order.OrderStatus,
                FullName = order.FullName,
                TelephoneNumber = order.TelephoneNumber,
                OrderAddress = order.OrderAddress,
                WilayaID = order.WilayaID,
                CommuneID = order.CommuneID,
                Wilaya = order.Wilaya,
                Commune = order.Commune,

                ShippingID = order.ShippingID,
                ShippingStatus = order.ShippingStatus,

                ShippingInfo = new ShippingInfoDTO
                {
                    ShippingID = order.ShippingInfo.ShippingID,
                    WilayaID = order.ShippingInfo.WilayaID,
                    ShipingStatus = order.ShippingInfo.ShipingStatus,


                    HomeDeliveryPrice = order.ShippingInfo.HomeDeliveryPrice,
                    OfficeDeliveryPrice = order.ShippingInfo.OfficeDeliveryPrice


                },
                OrderItems = order.OrderItems.Select(oi => new OrderItemsDTO
                {
                    OrderItemsID = oi.OrderItemsID,

                    Color = oi.Color,
                    Size = oi.Size,

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
        //[Authorize(Roles = "Writer")]

        public async Task<IActionResult> CreateOrderAsync([FromBody] AddOrderRequestDTO addOrderRequestDTO)
        {
            try
            {


                var OrderDomainModel = new Orders
                {
                    //OrderDate = addOrderRequestDTO.OrderDate,
                    //TotalPrice = addOrderRequestDTO.TotalPrice,
                    //OrderStatus = addOrderRequestDTO.OrderStatus,

                    FullName = addOrderRequestDTO.FullName,
                    TelephoneNumber = addOrderRequestDTO.TelephoneNumber,
                    OrderAddress = addOrderRequestDTO.OrderAddress,
                    WilayaID = addOrderRequestDTO.WilayaID,
                    CommuneID = addOrderRequestDTO.CommuneID,
                    //Wilaya = addOrderRequestDTO.Wilaya,
                    //Commune = addOrderRequestDTO.Commune,

                    //DiscountCodeID = addOrderRequestDTO.DiscountCodeID,
                    ShippingID = addOrderRequestDTO.ShippingID,
                    ShippingStatus = addOrderRequestDTO.ShippingStatus,




                    OrderItems = addOrderRequestDTO.OrderItems.Select(item => new OrderItems
                    {
                        ProductID = item.ProductID,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        Color = item.Color,
                        Size = item.Size,

                        TotalItemsPrice = item.TotalItemsPrice
                    }).ToList()


                };


                var newOrder = await ordersRepository.CreateAsync(OrderDomainModel);



                var OrdersDto = new OrdersDto
                {

                    OrderID = newOrder.OrderID,
                    OrderDate = newOrder.OrderDate,
                    
                    OrderStatus = newOrder.OrderStatus,
                    FullName = newOrder.FullName,
                    TelephoneNumber = newOrder.TelephoneNumber,
                    OrderAddress = newOrder.OrderAddress,
                    WilayaID = newOrder.WilayaID,
                    CommuneID = newOrder.CommuneID,

                    Wilaya = newOrder.Wilaya,
                    Commune = newOrder.Commune,
                    ShippingID = newOrder.ShippingID,
                    ShippingStatus = newOrder.ShippingStatus,

                    ShippingInfo = new ShippingInfoDTO
                    {
                        ShippingID = newOrder.ShippingInfo.ShippingID,
                        WilayaID = newOrder.ShippingInfo.WilayaID,
                        ShipingStatus = newOrder.ShippingInfo.ShipingStatus,

                        HomeDeliveryPrice = newOrder.ShippingInfo.HomeDeliveryPrice,
                        OfficeDeliveryPrice = newOrder.ShippingInfo.OfficeDeliveryPrice

                    },

                    OrderItems = newOrder.OrderItems.Select(x => new OrderItemsDTO
                    {
                        OrderItemsID = x.OrderItemsID,
                        OrderID = x.OrderID,
                        ProductID = x.ProductID,
                        Color = x.Color,
                        Size = x.Size,


                        Quantity = x.Quantity,
                        Price = x.Price,
                        TotalItemsPrice = x.TotalItemsPrice,
                        ProductName = x.ProductCatalog?.ProductName
                    }).ToList()



                    //DiscountCodeID = newOrder.DiscountCodeID,

                    //ShippingInfo = new ShippingInfoDTO
                    //{
                    //    ShippingID = newOrder.ShippingInfo.ShippingID,

                    //    ShipingStatus = newOrder.ShippingInfo.ShipingStatus,

                    //    WilayaID =newOrder.WilayaID,



                    //    //WilayaTo = newOrder.ShippingInfo.WilayaTo,

                    //    HomeDeliveryPrice = newOrder.ShippingInfo.HomeDeliveryPrice,
                    //    OfficeDeliveryPrice = newOrder.ShippingInfo.OfficeDeliveryPrice


                    //},




                };




                if (newOrder == null)
                {
                    return NotFound(new { Message = $"Order with ID {newOrder.OrderID} not found." });
                }


                return Created($"/api/Orders/{OrdersDto.OrderID}", OrdersDto);













            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });


            }


          

        }


        [HttpDelete("{id}")]
        //[Authorize(Roles = "Writer")]

        public async Task<IActionResult> DeleteOrderAsync(int id)
        {
            var order = await ordersRepository.DeleteAsync(id);

            if (order == null)
            {
                return NotFound();
            }


            var ordersDto = new OrdersDto
            {
                OrderID = order.OrderID,
                OrderDate = order.OrderDate,

                OrderStatus = order.OrderStatus,
                FullName = order.FullName,
                TelephoneNumber = order.TelephoneNumber,
                OrderAddress = order.OrderAddress,
                WilayaID = order.WilayaID,
                CommuneID = order.CommuneID,
                
                ShippingID = order.ShippingID,
                ShippingStatus = order.ShippingStatus,

                ShippingInfo = new ShippingInfoDTO
                {
                    ShippingID = order.ShippingInfo.ShippingID,
                    WilayaID = order.ShippingInfo.WilayaID,
                    ShipingStatus = order.ShippingInfo.ShipingStatus,

                    HomeDeliveryPrice = order.ShippingInfo.HomeDeliveryPrice,
                    OfficeDeliveryPrice = order.ShippingInfo.OfficeDeliveryPrice


                },
                OrderItems = order.OrderItems.Select(oi => new OrderItemsDTO
                {
                    OrderItemsID = oi.OrderItemsID,

                    Color = oi.Color,
                    Size = oi.Size,

                    ProductID = oi.ProductID,
                    ProductName = oi.ProductCatalog.ProductName,
                    Quantity = oi.Quantity,
                    Price = oi.Price,
                    TotalItemsPrice = oi.TotalItemsPrice
                }).ToList()
            };








            //var orderDto = mapper.Map<OrdersDto>(order);

            //var orderDto = new OrdersDto
            //{
            //    OrderID = order.OrderID,
            //    OrderDate = order.OrderDate,
            //      //TotalPrice = order.TotalPrice,
            //    OrderStatus = order.OrderStatus,
            //    FullName = order.FullName,
            //    TelephoneNumber = order.TelephoneNumber,
            //    OrderAddress = order.OrderAddress,
            //    Wilaya = order.Wilaya,
            //    Commune = order.Commune,
            //    //DiscountCodeID = order.DiscountCodeID,
            //    ShippingID = order.ShippingID,
            //    ShippingStatus = order.ShippingStatus,
            //    ShippingInfo = new ShippingInfoDTO
            //    {
            //        ShippingID = order.ShippingInfo.ShippingID,
            //        WilayaFrom = order.ShippingInfo.WilayaFrom,
            //        WilayaTo = order.ShippingInfo.WilayaTo,

            //        HomeDeliveryPrice = order.ShippingInfo.HomeDeliveryPrice,
            //        OfficeDeliveryPrice = order.ShippingInfo.OfficeDeliveryPrice


            //    },
            //    OrderItems = order.OrderItems.Select(oi => new OrderItemsDTO
            //    {
            //        OrderItemsID = oi.OrderItemsID,
            //        Color = oi.Color,
            //        Size = oi.Size,
            //        ProductID = oi.ProductID,
            //        ProductName=oi.ProductCatalog.ProductName,
            //        Quantity = oi.Quantity,
            //        Price = oi.Price,
            //        TotalItemsPrice = oi.TotalItemsPrice
            //    }).ToList()
            //};

            return Ok(ordersDto);
        }


        [HttpPut("{id}")]

        //[Authorize(Roles = "Reader")]

        public async Task<IActionResult> UpdateOrderAsync(int id, [FromBody] UpdateOrderRequestDTO addOrderRequestDTO)
        {
            var order = await ordersRepository.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            var ordertest= new Orders();

            ordertest.OrderDate = addOrderRequestDTO.OrderDate;
           ordertest.TotalPrice = addOrderRequestDTO.TotalPrice;
            ordertest.OrderStatus = addOrderRequestDTO.OrderStatus;
            ordertest.FullName = addOrderRequestDTO.FullName;
            ordertest.TelephoneNumber = addOrderRequestDTO.TelephoneNumber;
            ordertest.OrderAddress = addOrderRequestDTO.OrderAddress;
            ordertest.WilayaID = addOrderRequestDTO.WilayaID;
            ordertest.CommuneID = addOrderRequestDTO.CommuneID;

            //ordertest.Wilaya = addOrderRequestDTO.Wilaya;
            //ordertest.Commune = addOrderRequestDTO.Commune;
            //ordertest.DiscountCodeID = addOrderRequestDTO.DiscountCodeID;
            ordertest.ShippingID = addOrderRequestDTO.ShippingID;
            ordertest.ShippingStatus = addOrderRequestDTO.ShippingStatus;

            // here is the problem men 

            ordertest.OrderItems = addOrderRequestDTO.OrderItems.Select(item => new OrderItems
            {
                OrderItemsID= item.OrderItemsID,
                ProductID = item.ProductID,
                Quantity = item.Quantity,
                Size = item.Size,
                Color = item.Color,


            }).ToList();

            var updatedOrder = await ordersRepository.UpdateAsync(id, ordertest);

            var orderDto = new OrdersDto
            {
                OrderID = updatedOrder.OrderID,
                OrderDate = updatedOrder.OrderDate,
               
                OrderStatus = updatedOrder.OrderStatus,
                FullName = updatedOrder.FullName,
                TelephoneNumber = updatedOrder.TelephoneNumber,
                OrderAddress = updatedOrder.OrderAddress,
               
                ShippingID = updatedOrder.ShippingID,
                ShippingStatus = updatedOrder.ShippingStatus,
                ShippingInfo = new ShippingInfoDTO
                {
                   ShippingID =order.ShippingInfo.ShippingID,

                   WilayaID=order.ShippingInfo.WilayaID,

                    ShipingStatus = order.ShippingInfo.ShipingStatus,

                    HomeDeliveryPrice = order.ShippingInfo.HomeDeliveryPrice,
                    OfficeDeliveryPrice = order.ShippingInfo.OfficeDeliveryPrice


                },
                OrderItems = updatedOrder.OrderItems.Select(oi => new OrderItemsDTO
                {
                    OrderItemsID = oi.OrderItemsID,
                    ProductID = oi.ProductID,
                    ProductName = oi.ProductCatalog.ProductName,
                    Quantity = oi.Quantity,
                    Size =oi.Size,
                    Color=oi.Color,
                    Price = oi.ProductCatalog.Price,
                    TotalItemsPrice = oi.Quantity * oi.ProductCatalog.Price
                }).ToList()
            };

            return Ok(orderDto);
        }







    }
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
