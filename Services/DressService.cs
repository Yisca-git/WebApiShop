using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DTOs;
using Entities;
using Repositories;

namespace Services
{
    public class DressService : IDressService
    {
        private readonly IDressRepository _dressRepository;
        private readonly IModelService _modelService;
        private readonly IMapper _mapper;


        public DressService(IDressRepository dressRepository, IModelService modelService, IMapper mapper)
        {
            _dressRepository = dressRepository;
            _modelService = modelService;
            _mapper = mapper;
        }
        public async Task<DressDTO> GetDressById(int id)
        {
            Dress dress = await _dressRepository.GetDressById(id);
            if (dress == null)
            {
                throw new Exception($"Dress with ID {id} not found");
            }
            DressDTO dressDTO = _mapper.Map<Dress, DressDTO>(dress);
            return dressDTO;
        }
        public async Task<DressDTO> AddDress(NewDressDTO newDress)
        {
            if (newDress == null)
                throw new ArgumentNullException(nameof(newDress), "NewDressDTO cannot be null.");

            if (await _modelService.GetModelById(newDress.ModelId) == null)
                throw new ArgumentException($"Model with ID {newDress.ModelId} not found.");

            if (newDress.Price <= 0)
                throw new ArgumentException("Price must be greater than 0.");
            
            Dress dress = _mapper.Map<NewDressDTO, Dress>(newDress);
            Dress addedDress = await _dressRepository.AddDress(dress);
            DressDTO addedDressDTO = _mapper.Map<Dress, DressDTO>(addedDress);
            return addedDressDTO;
        }
        public async Task UpdateDress(int id, DressDTO updateDress)
        {
            Dress updeteDrs = _mapper.Map<DressDTO, Dress>(updateDress);
            if (updeteDrs == null)
                throw new ArgumentNullException(nameof(updeteDrs));

            if (await _dressRepository.GetDressById(id) == null)
                throw new KeyNotFoundException($"Dress with ID {id} not found.");

            if (await _modelService.GetModelById(updeteDrs.ModelId) == null)
                throw new KeyNotFoundException($"Dress with Model ID {updeteDrs.ModelId} not found.");

            if (updeteDrs.Price <= 0)
                throw new ArgumentException("Price must be greater than 0.");
            await _dressRepository.UpdateDress(updeteDrs);
        }
        public async Task DeleteDress(DressDTO deleteDress)
        {
            if (!deleteDress.IsActive)
                throw new InvalidOperationException("Dress is already inactive.");

            if (await _dressRepository.GetDressById(deleteDress.Id) == null)
                throw new KeyNotFoundException($"Dress with ID {deleteDress.Id} not found.");
            
            Dress deletDrs = _mapper.Map<DressDTO, Dress>(deleteDress);
            deletDrs.IsActive = false;
            await _dressRepository.DeleteDress(deletDrs);
        }
        public async Task<int> GetCountByModelIdAndSizeForDate(int id, string size, DateOnly date)
        {

            if (date < DateOnly.FromDateTime(DateTime.Now))
            {
                throw new Exception("Date must be in the future");
            }
            if (_modelService.GetModelById(id) == null)
            {
                throw new Exception("Model not found");
            }
            return await _dressRepository.GetCountByModelIdAndSizeForDate(id, size, date);
        }
        public async Task<List<string>> GetSizesByModelId(int id)
        {
            if (_modelService.GetModelById(id) == null)
            {
                throw new Exception("Model not found");
            }
            return await _dressRepository.GetSizesByModelId(id);

        }
    }
}
