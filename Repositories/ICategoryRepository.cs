using Entities;

namespace Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategoryById(int id);
        Task<Category> AddCategory(Category category);
    }
}