using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entities;
using Entities.DTO;
using Repositories;
namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;

        }

        public async Task<IEnumerable<ProductDTO>> GetProducts(string? Description, int? minPrice,
                       int? maxPrice, int?[] categoriesId, int  ? position , int  ? skip)
        {
            IEnumerable<Product> products = await _productRepository.GetProducts(Description, minPrice, maxPrice, categoriesId, position, skip);
            IEnumerable<ProductDTO> productDTOs = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
            return productDTOs;
        }
        public async Task<ProductDTO> GetProductById(int id)
        {
            Product product = await _productRepository.GetProductById(id);
            ProductDTO productDTO = _mapper.Map<Product, ProductDTO>(product);
            return productDTO;
        }
    }
}
