﻿using AutoMapper;
using EcommerceWeb.Api.Data;
using EcommerceWeb.Api.Models.Domain;
using EcommerceWeb.Api.Models.DTO;
using EcommerceWeb.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly EcommerceDbContext dbContext;
        private readonly IProductRepository productRepository;

        private readonly IProductImageRepository productImageRepository;

        private readonly IMapper mapper;

        public ProductsController(EcommerceDbContext dbContext,IProductRepository productRepository, IProductImageRepository productImageRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.productRepository = productRepository;
            this.productImageRepository = productImageRepository;
            this.mapper = mapper;
        }

        [HttpGet]

        public async  Task<IActionResult> GetAllAsync([FromQuery] string ? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber=1, [FromQuery] int pagesize=1000)
        {
           
            var products = await productRepository.GetAllAsync(filterOn, filterQuery,  sortBy, isAscending, pageNumber, pagesize);

            var productDTOList = mapper.Map<List<ProductDTO>>(products);
            return Ok(productDTOList);
        }

        [HttpGet]
        [Route("{id}")]

        public async Task< IActionResult> GetByIdAsync([FromRoute]int id)
        {
       
            var product = await productRepository.GetByIdAsync(id);
       
            if (product == null)
            {
                return NotFound();
            }

          

            var productDto = mapper.Map<ProductDTO>(product);



            return Ok(productDto);
        }



        [HttpPost]

       [Authorize(Roles = "Writer")]

        public async  Task <IActionResult> CreateAsync([FromForm] ProductUplodadImageDTO AddProductRequestDTO)
        {

         


            var productDomainModel = new ProductCatalog
           {
               ProductName = AddProductRequestDTO.ProductName,
               Description = AddProductRequestDTO.Description,
               Price = AddProductRequestDTO.Price,
               Discount = AddProductRequestDTO.Discount,
               Stock = AddProductRequestDTO.Stock,
               CreatedAt = DateTime.Now,
               CategoryID = AddProductRequestDTO.CategoryID,

               ProductImages = AddProductRequestDTO.ImageFile.Select((item, index) => new ProductImages
               {
                   ImageOrder = index,
                   ImageFile = item,



               }).ToList()


           };

            productDomainModel = await productRepository.CreateAsync(productDomainModel);

           

            var productDTO = mapper.Map<ProductDTO>(productDomainModel);





            return Created($"/api/Products/{productDTO.ProductID}", productDTO);
        }


        

        [HttpPut]
        [Route("{id}")]

       [Authorize(Roles = "Reader")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromForm] ProductUplodadImageDTO updateProductRequestDTO)
        {

          var product = new ProductCatalog();


            product.ProductName = updateProductRequestDTO.ProductName;
            product.Description = updateProductRequestDTO.Description;
            product.Price = updateProductRequestDTO.Price;
            product.Discount = updateProductRequestDTO.Discount;
            product.Stock = updateProductRequestDTO.Stock;
            product.CreatedAt = DateTime.Now;
            product.CategoryID = updateProductRequestDTO.CategoryID;



            product.ProductImages=updateProductRequestDTO.ImageFile.Select((item, index) => new ProductImages
            {
                ImageOrder = index,
                ImageFile = item,

            }).ToList();





            product = await productRepository.UpdateAsync(id, product);


            if(product == null)
            {
                return NotFound();
            }


           


           

            var productDTO = mapper.Map<ProductDTO>(product);

            return Ok(productDTO);

        }


        [HttpDelete]
        [Route("{id}")]

        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var product = await productRepository.DeleteAsync(id);



            if (product == null)
            {
                return NotFound();
            }

       


            var productDTO = mapper.Map<ProductDTO>(product);   



            return Ok(productDTO);
        }









    }
}
