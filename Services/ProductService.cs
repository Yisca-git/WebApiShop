using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Repositories;
namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;

        }

        public async Task<IEnumerable<Product>> GetProducts(string? Description, int? minPrice,
                       int? maxPrice, int?[] categoriesId, int  ? position , int  ? skip)
        {
            return await _productRepository.GetProducts(Description, minPrice, maxPrice, categoriesId, position, skip);
        }
        public async Task<Product> GetProductById(int id)
        {
            return await _productRepository.GetProductById(id);
        }
    }
}
