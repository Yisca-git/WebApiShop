using Entities;
using DTOs;

namespace Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetCategories();
        Task<CategoryDTO> AddCategory(CategoryDTO NewCategory);
    }
}