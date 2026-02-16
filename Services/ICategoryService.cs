using Entities;
using DTOs;

namespace Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetCategories();
        Task<NewCategoryDTO> GetCategoryById(int id);
        Task<NewCategoryDTO> AddCategory(CategoryDTO NewCategory);
        Task<bool> IsExistsCategoryById(int id);
    }
}