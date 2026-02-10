using Entities;

namespace Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
        Task<Category> AddCategory(Category category);
    }
}