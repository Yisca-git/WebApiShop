using Entities;

namespace Repositories
{
    public interface IModelRepository
    {
        public Task<(List<Model> items, int TotalCount)> GetModels(string? description, int? minPrice,
                        int? maxPrice, int[] categoriesId, string? color, int position = 1, int skip = 8);
       public Task<Model> GetModelById(int id);
       public Task<Model> AddModel(Model model);
       public Task DeleteModel(Model model);
       public Task UpdateModel(Model model);
        Task<bool> IsExistsModelById(int id);
    }
}