using DTOs;

namespace Services
{
    public interface IModelService
    {
        Task<ModelDTO> AddModel(NewModelDTO NewModel);
        Task DeleteMoedl(ModelDTO deleteModel);
        Task<ModelDTO> GetModelById(int id);
        Task<FinalModels> GetModels(string? Description, int? minPrice, int? maxPrice, int[] categoriesId, string? color, int position = 1, int skip = 8);
        Task UpdateModel(int id, ModelDTO updateModel);
    }
}