using AutoMapper;
using EcommerceWeb.Api.Data;
using EcommerceWeb.Api.Models.Domain;
using EcommerceWeb.Api.Models.DTO;
using EcommerceWeb.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
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

        private readonly IMapper mapper;

        public ShippingInfoController(EcommerceDbContext dbContext , IShippingInfoRepository ShippingInfoRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.ShippingInfoRepository = ShippingInfoRepository;
            this.mapper=mapper;


            
        }

        [HttpGet]

        public async Task<IActionResult> GetAllAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pagesize = 1000)
        {
            var ShippingInfo = await ShippingInfoRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pagesize);

            var ShippingInfoList = mapper.Map<List<ShippingInfoDTO>>(ShippingInfo);



            return Ok(ShippingInfoList);



        }


        [HttpGet]
        [Route("{id}")]

        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var shippingInfo = await ShippingInfoRepository.GetByIdAsync(id);

            if (shippingInfo == null)
            {
                return NotFound();
            }



            var shipingDTO = mapper.Map<ShippingInfoDTO>(shippingInfo);

            return Ok(shipingDTO);




        }


        [HttpGet]
        [Route("communes/{id}")]

        public async Task<IActionResult> GetCommunOfWilaya([FromRoute] int id)
        {
            var Communes = await ShippingInfoRepository.GetCommunOfWilaya(id);
            if (Communes == null)
            {
                return NotFound();
            }
            var communesDTO = mapper.Map<List<CommuneDTO>>(Communes);

            return Ok(communesDTO);






        }







        [HttpPost]
        //[Authorize(Roles = "Writer")]

        public async Task<IActionResult> CreateAsync([FromBody] AddShippingRequestDTO addShippingRequestDTO)
        {



            var shippingDomainModal = mapper.Map<ShippingInfo>(addShippingRequestDTO);




            shippingDomainModal = await ShippingInfoRepository.CreateAsync(shippingDomainModal);


            var shipingDTO = mapper.Map<ShippingInfoDTO>(shippingDomainModal);

            return Created($"/api/ShippingInfo/{shipingDTO.ShippingID}", shipingDTO);



        }


        [HttpPut]
        [Route("{id}")]
        //[Authorize(Roles = "Reader")]

        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateShippingRequestDTO UpdateShippingRequestDTO)
        {

            var shippinginfo = new ShippingInfo();

           
           
            shippinginfo.ShipingStatus = UpdateShippingRequestDTO.ShipingStatus;
            shippinginfo.OfficeDeliveryPrice = UpdateShippingRequestDTO.OfficeDeliveryPrice;
            shippinginfo.HomeDeliveryPrice = UpdateShippingRequestDTO.HomeDeliveryPrice;

            shippinginfo = await ShippingInfoRepository.UpdateAsync(id, shippinginfo);

            if (shippinginfo == null)
            {
                return NotFound();
            }



            var shipingDTO = mapper.Map<ShippingInfoDTO>(shippinginfo);







            return Ok(shipingDTO);



        }

        [HttpDelete]
        [Route("{id}")]
        //[Authorize(Roles = "Writer")]

        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var shippinginfo = await ShippingInfoRepository.DeleteAsync(id);

            if (shippinginfo == null)
            {


                return NotFound();
            }

            var shipingDTO = mapper.Map<ShippingInfoDTO>(shippinginfo);

            return Ok(shipingDTO);



        }



      }
}
