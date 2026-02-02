using Entities;
using Entities.DTOs;

namespace Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetCategories();
    }
}