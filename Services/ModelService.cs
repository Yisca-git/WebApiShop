using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entities;
using DTOs;
using Repositories;
namespace Services
{
    public class ModelService : IModelService
    {
        private readonly IModelRepository _modelRepository;
        private readonly IDressService _dressService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ModelService(IModelRepository modelRepository, IDressService dressService, ICategoryService categoryService, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _dressService = dressService;
            _categoryService = categoryService;
            _mapper = mapper;

        }
        public bool CheckModel(NewModelDTO newModel)
        {
            return newModel != null;
        }
        public bool CheckModel(ModelDTO model)
        {
            return model != null;
        }
        public bool CheckBasePrice(int price)
        {
            return price > 0;
        }
        public bool ValidateQueryParameters(int position, int skip, int? minPrice, int? maxPrice)
        {
            if (minPrice.HasValue && maxPrice.HasValue)
                return position >= 0 && skip >= 0 && minPrice < maxPrice;
            return position >= 0 && skip >= 0;
        }
        public async Task<bool> CheckCategories(List<NewCategoryDTO> categories)
        {
            foreach (var category in categories)
            {
                if (!await _categoryService.IsExistsCategoryById(category.Id))
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<FinalModels> GetModels(string? Description, int? minPrice,
                       int? maxPrice, int[] categoriesId, string? color, int position = 1, int skip = 8)
        {
            (List<Model> items, int TotalCount) models = await _modelRepository.GetModels(Description, minPrice, maxPrice, categoriesId, color, position, skip);
            List<ModelDTO> modelsDTOs = _mapper.Map<List<Model>, List<ModelDTO>>(models.items);
            bool hasNext = (models.TotalCount - (position * skip)) > 0;
            bool hasPrev = position > 1;
            FinalModels finalModels = new()
            {
                Models = modelsDTOs,
                TotalCount = models.TotalCount,
                HasNext = hasNext,
                HasPrev = hasPrev
            };
            return finalModels;
        }
        public async Task<ModelDTO> GetModelById(int id)
        {
            Model model = await _modelRepository.GetModelById(id);
            if (model == null)
            {
                return null;
            }
            ModelDTO modelDTO = _mapper.Map<Model, ModelDTO>(model);
            return modelDTO;
        }
        public async Task<ModelDTO> AddModel(NewModelDTO newModel)
        {
            Model model = _mapper.Map<NewModelDTO, Model>(newModel);
            model.IsActive = true;
            Model addedModel = await _modelRepository.AddModel(model);
            ModelDTO addedModelDTO = _mapper.Map<Model, ModelDTO>(addedModel);
            return addedModelDTO;
        }
        public async Task DeleteMoedl(ModelDTO deleteModel)
        {
            Model deleteMdl = _mapper.Map<ModelDTO, Model>(deleteModel);
            deleteMdl.IsActive = false;
            foreach (var dress in deleteMdl.Dresses)
            {
                DressDTO dressDTO = _mapper.Map<Dress, DressDTO>(dress);
                await _dressService.DeleteDress(dressDTO);
            }
            await _modelRepository.DeleteModel(deleteMdl);
        }
        public async Task UpdateModel(ModelDTO updateModel)
        {
            Model updeteMdl = _mapper.Map<ModelDTO, Model>(updateModel);
            await _modelRepository.UpdateModel(updeteMdl);
        }
        public async Task<bool> IsExistsModelById(int id)
        {
            return await _modelRepository.IsExistsModelById(id);
        }
    }
}
