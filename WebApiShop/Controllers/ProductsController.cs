using Microsoft.AspNetCore.Mvc;
using Entities;
using Entities.DTO;
using Repositories;
using Services;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts(string? Description, int? minPrice,
                       int? maxPrice,[FromQuery] int?[] categoriesId, int ? position, int ? skip)
        {
            IEnumerable<ProductDTO> products = await _productService.GetProducts(Description, minPrice, maxPrice, categoriesId, position, skip);
            if (products.Count() == 0)
            {
                return NoContent();
            }
            return Ok(products);
   
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            ProductDTO product = await _productService.GetProductById(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        // POST api/<ProductsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
