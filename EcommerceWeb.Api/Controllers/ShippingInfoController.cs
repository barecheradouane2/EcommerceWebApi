using EcommerceWeb.Api.Data;
using EcommerceWeb.Api.Models.Domain;
using EcommerceWeb.Api.Models.DTO;
using EcommerceWeb.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Linq.Expressions;

namespace EcommerceWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingInfoController : ControllerBase
    {
        private readonly EcommerceDbContext dbContext;

        private readonly IShippingInfoRepository ShippingInfoRepository;

        public ShippingInfoController(EcommerceDbContext dbContext , IShippingInfoRepository ShippingInfoRepository)
        {
            this.dbContext = dbContext;
            this.ShippingInfoRepository = ShippingInfoRepository;


            
        }

        [HttpGet]

        public async Task<IActionResult> GetAllAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pagesize = 1000)
        {
            var ShippingInfo = await ShippingInfoRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pagesize);

            var ShippingInfoList = new List<ShippingInfoDTO>();

            foreach(var shippingInfo in ShippingInfo)
            {
                ShippingInfoList.Add(

                    new ShippingInfoDTO
                    {
                        ShippingID = shippingInfo.ShippingID,
                        WilayaFrom = shippingInfo.WilayaFrom,
                        WilayaTo = shippingInfo.WilayaTo,
                        ShipingStatus = shippingInfo.ShipingStatus,
                        HomeDeliveryPrice = shippingInfo.HomeDeliveryPrice,
                        OfficeDeliveryPrice = shippingInfo.OfficeDeliveryPrice,


                    });


            }

            return Ok(ShippingInfoList);

        }


        [HttpGet]
        [Route("{id}")]

        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var shippingInfo = await ShippingInfoRepository.GetByIdAsync(id);

            if(shippingInfo == null)
            {
                return NotFound();
            }

            var shipingDTO = new ShippingInfoDTO { 

                ShippingID = shippingInfo.ShippingID,
                WilayaFrom= shippingInfo.WilayaFrom,
                WilayaTo = shippingInfo.WilayaTo,
                ShipingStatus = shippingInfo.ShipingStatus,
                HomeDeliveryPrice = shippingInfo.HomeDeliveryPrice,
                OfficeDeliveryPrice = shippingInfo.OfficeDeliveryPrice,

            };

            return Ok(shipingDTO);


        }




        [HttpPost]

        public async Task<IActionResult> CreateAsync([FromBody] AddShippingRequestDTO addShippingRequestDTO)
        {
            var shippingDomainModal = new ShippingInfo {

                WilayaFrom = addShippingRequestDTO.WilayaFrom,
                WilayaTo = addShippingRequestDTO.WilayaTo,
                ShipingStatus = addShippingRequestDTO.ShipingStatus,
                HomeDeliveryPrice = addShippingRequestDTO.HomeDeliveryPrice,
                OfficeDeliveryPrice = addShippingRequestDTO.OfficeDeliveryPrice,

            };

            shippingDomainModal=await ShippingInfoRepository.CreateAsync(shippingDomainModal);


            var shipingDTO = new ShippingInfoDTO
            {

                ShippingID = shippingDomainModal.ShippingID,
                WilayaFrom = shippingDomainModal.WilayaFrom,
                WilayaTo = shippingDomainModal.WilayaTo,
                ShipingStatus = shippingDomainModal.ShipingStatus,
                HomeDeliveryPrice = shippingDomainModal.HomeDeliveryPrice,
                OfficeDeliveryPrice = shippingDomainModal.OfficeDeliveryPrice,

            };

            return Created($"/api/ShippingInfo/{shipingDTO.ShippingID}", shipingDTO);



        }


        [HttpPut]
        [Route("{id}")]

        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] AddShippingRequestDTO addShippingRequestDTO)
        {

            var shippinginfo = new ShippingInfo();

            shippinginfo.WilayaFrom = addShippingRequestDTO.WilayaFrom;
            shippinginfo.WilayaTo = addShippingRequestDTO.WilayaTo;
            shippinginfo.ShipingStatus = addShippingRequestDTO.ShipingStatus;
            shippinginfo.OfficeDeliveryPrice = addShippingRequestDTO.OfficeDeliveryPrice;
            shippinginfo.HomeDeliveryPrice = addShippingRequestDTO.HomeDeliveryPrice;

            shippinginfo=await ShippingInfoRepository.UpdateAsync(id, shippinginfo);

            if (shippinginfo == null)
            {
                return NotFound();
            }

            var shipingDTO = new ShippingInfoDTO
            {

                ShippingID = shippinginfo.ShippingID,
                WilayaFrom = shippinginfo.WilayaFrom,
                WilayaTo = shippinginfo.WilayaTo,
                ShipingStatus = shippinginfo.ShipingStatus,
                HomeDeliveryPrice = shippinginfo.HomeDeliveryPrice,
                OfficeDeliveryPrice = shippinginfo.OfficeDeliveryPrice,

            };

            return Ok(shipingDTO);

        }

        [HttpDelete]
        [Route("{id}")]

        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var shippinginfo = await ShippingInfoRepository.DeleteAsync(id);

            if(shippinginfo == null) {


                return NotFound();
              }

            var shipingDTO = new ShippingInfoDTO
            {

                ShippingID = shippinginfo.ShippingID,
                WilayaFrom = shippinginfo.WilayaFrom,
                WilayaTo = shippinginfo.WilayaTo,
                ShipingStatus = shippinginfo.ShipingStatus,
                HomeDeliveryPrice = shippinginfo.HomeDeliveryPrice,
                OfficeDeliveryPrice = shippinginfo.OfficeDeliveryPrice,

            };

            return Ok(shipingDTO);



        }



      }
}
