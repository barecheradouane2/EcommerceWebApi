using EcommerceWeb.Api.Data;
using EcommerceWeb.Api.Models.Domain;
using EcommerceWeb.Api.Models.DTO;
using EcommerceWeb.Api.Repositories;
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
        public ProductsController(EcommerceDbContext dbContext,IProductRepository productRepository, IProductImageRepository productImageRepository)
        {
            this.dbContext = dbContext;
            this.productRepository = productRepository;
            this.productImageRepository = productImageRepository;
        }

        [HttpGet]

        public async  Task<IActionResult> GetAllAsync([FromQuery] string ? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber=1, [FromQuery] int pagesize=1000)
        {
            // get 
            var products = await productRepository.GetAllAsync(filterOn, filterQuery,  sortBy, isAscending, pageNumber, pagesize);
            // map to dto


            var productDTO = new List<ProductDTO>();

            foreach (var product in products)
            {
                productDTO.Add(new ProductDTO
                {
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Price = product.Price,
                    Discount = product.Discount,
                    CreatedDate = product.CreatedAt,
                    Category=product.Category != null ? new CategoryDTO
                    {
                        CategoryID = product.Category.CategoryID,
                        CategoryName = product.Category.CategoryName,
                        Description = product.Category.Description
                    } : null


                });
            }
            return Ok(productDTO);
        }

        [HttpGet]
        [Route("{id}")]

        public async Task< IActionResult> GetByIdAsync([FromRoute]int id)
        {
            // this is can use only in the id property
            var product = await productRepository.GetByIdAsync(id);
            // dbcontext.ProductCatalog.FirstOrDefault(p => p.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            var productDto = new ProductDTO
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                Discount = product.Discount,
                CreatedDate = product.CreatedAt,
                Category = product.Category != null ? new CategoryDTO
                {
                    CategoryID = product.Category.CategoryID,
                    CategoryName = product.Category.CategoryName,
                    Description = product.Category.Description
                } : null
            };
            return Ok(productDto);
        }



        [HttpPost]

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
              CategoryID = AddProductRequestDTO.CategoryID




          };

            productDomainModel= await productRepository.CreateAsync(productDomainModel);

            var productDTO = new ProductDTO {

                ProductID = productDomainModel.ProductID,
                ProductName = productDomainModel.ProductName,
                Description = productDomainModel.Description,
                Price = productDomainModel.Price,
                Discount = productDomainModel.Discount,
                CreatedDate = productDomainModel.CreatedAt,
                Category = productDomainModel.Category != null ? new CategoryDTO
                {
                    CategoryID = productDomainModel.Category.CategoryID,
                    CategoryName = productDomainModel.Category.CategoryName,
                    Description = productDomainModel.Category.Description
                } : null



            };

            // set the productImagesModel
            var imageorder = 0;
            foreach (var imageFile in AddProductRequestDTO.ImageFile)
            {
                var productImagesModel = new ProductImages
                {
                    ProductID = productDomainModel.ProductID,
                    
                    ImageOrder = imageorder++,
                    ImageFile = imageFile

                };

                await productImageRepository.CreateAsync(productImagesModel);
            }



            return Created($"/api/Products/{productDTO.ProductID}", productDTO);
        }


        // update

        [HttpPut]
        [Route("{id}")]
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





            product = await productRepository.UpdateAsync(id, product);


            if(product == null)
            {
                return NotFound();
            }


            var imageorder = 0;
            foreach (var imageFile in updateProductRequestDTO.ImageFile)
            {
                var productImagesModel = new ProductImages
                {
                    ProductID = product.ProductID,

                    ImageOrder = imageorder++,
                    ImageFile = imageFile

                };

                await productImageRepository.UpdateAsync(product.ProductID, productImagesModel);
            }




















            var productDTO = new ProductDTO
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                Discount = product.Discount,
                CreatedDate = product.CreatedAt,
                
                Category = product.Category != null ? new CategoryDTO
                {
                    CategoryID = product.Category.CategoryID,
                    CategoryName = product.Category.CategoryName,
                    Description = product.Category.Description
                } : null
            };

            return Ok(productDTO);

        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var product = await productRepository.DeleteAsync(id);



            if (product == null)
            {
                return NotFound();
            }

            var productDTO= new ProductDTO
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                Discount = product.Discount,
                CreatedDate = product.CreatedAt,
                Category = product.Category != null ? new CategoryDTO
                {
                    CategoryID = product.Category.CategoryID,
                    CategoryName = product.Category.CategoryName,
                    Description = product.Category.Description
                } : null
            };


            for(int i = 0; i < 4; i++)
            {
                await productImageRepository.DeleteAsync(product.ProductID,i);
            }




            return Ok(productDTO);
        }

        [HttpGet("{id}/images")]

        public async Task<IActionResult> GetProductImagesAsync([FromRoute] int id)
        {
            var productImages = await productImageRepository.GetByIdAsync(id);

            if (productImages == null)
            {
                return NotFound();
            }

            var productImagesDTO = new List<ProductImagesDTO>();

            foreach (var productImage in productImages)
            {
                productImagesDTO.Add(new ProductImagesDTO
                {
                    ProductID = productImage.ProductID,
                    ImageID = productImage.ImageID,
                    ImageOrder = productImage.ImageOrder,
                    ImageUrl = productImage.ImageUrl
                });
            }

            return Ok(productImagesDTO);
        }






    }
}
