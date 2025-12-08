using Entities;

namespace Services
{
    public interface IProductService
    {
        Task<Product> GetProductById(int id);
        Task<IEnumerable<Product>> GetProducts(string? Description, int? minPrice, int? maxPrice, int?[] categoriesId, int ? position, int  ? skip);
    }
}