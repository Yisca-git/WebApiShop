using Entities;
using Entities.DTOs;

namespace Services
{
    public interface IProductService
    {
        Task<FinalProducts> GetProducts(string? Description, int? minPrice, int? maxPrice, int[] categoriesId, int position = 1, int skip = 8);

        Task<ProductDTO> GetProductById(int id);
    }
}