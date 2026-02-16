using DTOs;

namespace Services
{
    public interface IModelService
    {
        Task<ModelDTO> AddModel(NewModelDTO newModel);
        bool CheckBasePrice(int price);
        bool CheckModel(ModelDTO model);
        bool CheckModel(NewModelDTO newModel);
        Task DeleteMoedl(ModelDTO deleteModel);
        Task<ModelDTO> GetModelById(int id);
        Task<FinalModels> GetModels(string? Description, int? minPrice, int? maxPrice, int[] categoriesId, string? color, int position = 1, int skip = 8);
        Task UpdateModel(ModelDTO updateModel);
        bool ValidateQueryParameters(int position, int skip, int? minPrice, int? maxPrice);
    }
}