using EcommerceWeb.Api.Data;
using EcommerceWeb.Api.Models.Domain;
using EcommerceWeb.Api.Models.DTO;
using EcommerceWeb.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EcommerceWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class CategoryController : ControllerBase
    {

        private readonly EcommerceDbContext dbContext;
        private readonly ICategoryRepository categoryRepository;
        private readonly ILogger<CategoryController> logger;
        public CategoryController(EcommerceDbContext dbContext, ICategoryRepository categoryRepository,ILogger <CategoryController> logger)
        {
            this.dbContext = dbContext;
            this.categoryRepository = categoryRepository;
            this.logger = logger;
        }

        [HttpGet]
     //   [Authorize(Roles = "Reader,Writer")]

        public async Task<IActionResult> GetAllAsync()
        {
            logger.LogInformation("Get All Categories");

            var categories = await categoryRepository.GetAllAsync();


            var categoryDTO = new List<CategoryDTO>();

           foreach (var category in categories) {

                categoryDTO.Add(new CategoryDTO
                {
                    CategoryID = category.CategoryID,
                    CategoryName = category.CategoryName,
                    Description = category.Description
                });
            }

            logger.LogInformation($"Returning {categories.Count} categories {JsonSerializer.Serialize(categoryDTO)}");


            return Ok(categories);

        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var categoryDto = new CategoryDTO
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName,
                Description = category.Description
            };

            return Ok(categoryDto);
        }


        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateAsync([FromBody] AddCategoryRequestDTO AddCategoryRequestDTO)
        {

            if(ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var category = new Category
            {
                CategoryName = AddCategoryRequestDTO.CategoryName,
                Description = AddCategoryRequestDTO.Description
            };

            category= await categoryRepository.CreateAsync(category);


            var categoryDTO = new CategoryDTO
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName,
                Description = category.Description
            };




           // return CreatedAtAction(nameof(GetByIdAsync), new { controller = "Category", id = category.CategoryID }, categoryDTO);

            return Created($"/api/Category/{category.CategoryID}", categoryDTO);






        }


        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateCategoryDTO UpdateCategoryDTO)
        {

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }



            var category = new Category
            {
                CategoryName = UpdateCategoryDTO.CategoryName,
                Description = UpdateCategoryDTO.Description
            };

            category = await categoryRepository.UpdateAsync(id, category);

            var categoryDTO = new CategoryDTO
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName,
                Description = category.Description
            };

            return Ok(categoryDTO);

        }


        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
           var category= await categoryRepository.DeleteAsync(id);

            if(category == null)
            {
                return NotFound();
            }

            var categoryDTO = new CategoryDTO
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName,
                Description = category.Description
            };



            return Ok(categoryDTO);
        }










    }
}
