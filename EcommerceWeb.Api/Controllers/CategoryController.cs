using AutoMapper;
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
        private readonly IMapper mapper;    
        public CategoryController(EcommerceDbContext dbContext, ICategoryRepository categoryRepository,ILogger <CategoryController> logger,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.categoryRepository = categoryRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
       [Authorize(Roles = "Reader,Writer")]

        public async Task<IActionResult> GetAllAsync()
        {
            logger.LogInformation("Get All Categories");

            var categories = await categoryRepository.GetAllAsync();


           

            var categoryDTOList = mapper.Map<List<CategoryDTO>>(categories);

            logger.LogInformation($"Returning {categories.Count} categories {JsonSerializer.Serialize(categoryDTOList)}");

            


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

            

            var categoryDto = mapper.Map<CategoryDTO>(category);

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

          

            var category = mapper.Map<Category>(AddCategoryRequestDTO);

            category = await categoryRepository.CreateAsync(category);


           

            var categoryDTO = mapper.Map<CategoryDTO>(category);




         

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




            var category = mapper.Map<Category>(UpdateCategoryDTO);

            category = await categoryRepository.UpdateAsync(id, category);

           

            var categoryDTO = mapper.Map<CategoryDTO>(category);

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

            

            var categoryDTO = mapper.Map<CategoryDTO>(category);



            return Ok(categoryDTO);
        }










    }
}
