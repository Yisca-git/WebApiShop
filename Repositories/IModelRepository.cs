using Entities;

namespace Repositories
{
    public interface IModelRepository
    {
        public Task<(List<Product> items, int TotalCount)> GetProducts(string? description, int? minPrice,
                       int? maxPrice, int[] categoriesId, int  position = 1, int  skip = 8);
        public Task<Product> GetProductById(int id);
    }
}