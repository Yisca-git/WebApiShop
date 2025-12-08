using Entities;

namespace Repositories
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> GetProducts(string? Description, int? minPrice,
                       int? maxPrice, int?[] categoriesId, int ? position, int ? skip);
        public Task<Product> GetProductById(int id);
    }
}