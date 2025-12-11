using Entities;
using Entities.DTO;

namespace Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts(string? Description, int? minPrice, int? maxPrice, int?[] categoriesId, int ? position, int  ? skip);

        Task<ProductDTO> GetProductById(int id);
    }
}