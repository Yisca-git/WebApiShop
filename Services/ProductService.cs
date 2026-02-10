using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entities;
using Entities.DTOs;
using Repositories;
namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IModelRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IModelRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;

        }

        public async Task<FinalProducts> GetProducts(string? Description, int? minPrice,
                       int? maxPrice, int[] categoriesId, int position = 1 , int skip = 8)
        {
            (List<Product> items, int TotalCount) products = await _productRepository.GetProducts(Description, minPrice, maxPrice, categoriesId, position, skip);
            List<ProductDTO> productDTOs = _mapper.Map<List<Product>, List<ProductDTO>>(products.items);
            bool hasNext = (products.TotalCount - (position * skip)) > 0;
            bool hasPrev = position > 1;
            FinalProducts finalProducts = new()
            {
                Products = productDTOs,
                TotalCount = products.TotalCount,
                HasNext = hasNext,
                HasPrev = hasPrev
            };
            return finalProducts;
        }
        public async Task<ProductDTO> GetProductById(int id)
        {
            Product product = await _productRepository.GetProductById(id);
            ProductDTO productDTO = _mapper.Map<Product, ProductDTO>(product);
            return productDTO;
        }
    }
}
