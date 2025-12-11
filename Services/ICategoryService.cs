using Entities;
using Entities.DTO;

namespace Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetCategories();
    }
}