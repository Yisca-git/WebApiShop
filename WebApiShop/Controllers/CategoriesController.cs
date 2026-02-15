
using Microsoft.AspNetCore.Mvc;
using DTOs;
using Repositories;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetCategories()
        {
            List<CategoryDTO> categories = await _categoryService.GetCategories();
            if (categories.Count() == 0)
            {
                return NoContent();
            }
            return Ok(categories);
          
        }
        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NewCategoryDTO>> GetCategoryById(int id)
        {
            NewCategoryDTO category = await _categoryService.GetCategoryById(id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }
        // POST api/<Users>
        [HttpPost]
        public async Task<ActionResult<NewCategoryDTO>> AddCategory([FromBody] CategoryDTO newCategory)
        {
            NewCategoryDTO? category = await _categoryService.AddCategory(newCategory);
            return CreatedAtAction(nameof(GetCategoryById), new { Id = category.Id }, category);
        }
        

       
    }
}
