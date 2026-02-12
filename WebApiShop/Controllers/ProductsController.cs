using Microsoft.AspNetCore.Mvc;
using Entities;
using Entities.DTOs;
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
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<ActionResult<FinalProducts>> GetProducts(string? Description, int? minPrice,
                       int? maxPrice,[FromQuery] int[] categoriesId, int position =1 , int skip = 8)
        {
            FinalProducts products = await _productService.GetProducts(Description, minPrice, maxPrice, categoriesId, position, skip);
            if (products.Products.Count() == 0)
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
    }
}
