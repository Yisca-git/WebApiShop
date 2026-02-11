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
        private readonly IMapper _mapper;

        public ModelService(IModelRepository modelRepository, IDressService dressService, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _dressService = dressService;
            _mapper = mapper;

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
                throw new Exception("Model not found");
            }
            ModelDTO modelDTO = _mapper.Map<Model, ModelDTO>(model);
            return modelDTO;
        }
        public async Task<ModelDTO> AddModel(NewModelDTO NewModel)
        {
            if (NewModel.BasePrice <= 0)
            {
                return null;
            }
            Model model = _mapper.Map<NewModelDTO, Model>(NewModel);
            Model addedModel = await _modelRepository.AddModel(model);
            ModelDTO addedModelDTO = _mapper.Map<Model, ModelDTO>(addedModel);
            return addedModelDTO;
        }
        public async Task DeleteMoedl(ModelDTO deleteModel)
        {
            Model model = await _modelRepository.GetModelById(deleteModel.Id);
            if (model == null)
            {
                throw new Exception("Model not found");
            }
            Model deleteMdl = _mapper.Map<ModelDTO, Model>(deleteModel);
            deleteMdl.IsActive = false;
            foreach (var dress in deleteMdl.Dresses)
            {
                DressDTO dressDTO = _mapper.Map<Dress, DressDTO>(dress);
                await _dressService.DeleteDress(dressDTO);
            }
            await _modelRepository.DeleteModel(deleteMdl);
        }
        public async Task UpdateModel(int id, ModelDTO updateModel)
        {
            Model updeteMdl = _mapper.Map<ModelDTO, Model>(updateModel);
            if (updeteMdl.BasePrice <= 0)
            {
                return;
            }
            if (_modelRepository.GetModelById(updeteMdl.Id) == null)
            {
                return;
            }

            await _modelRepository.UpdateModel(updeteMdl);
        }

    }
}
