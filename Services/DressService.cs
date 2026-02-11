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
                throw new Exception("Dress not found");
            }
            DressDTO dressDTO = _mapper.Map<Dress, DressDTO>(dress);
            return dressDTO;
        }
        public async Task<DressDTO> AddDress(NewDressDTO NewDress)
        {
            if (NewDress.Price <= 0)
            {
                return null;
            }
            if (_modelService.GetModelById(NewDress.ModelId) == null)
            {
                return null;
            }
            Dress dress = _mapper.Map<NewDressDTO, Dress>(NewDress);
            Dress addedDress = await _dressRepository.AddDress(dress);
            DressDTO addedDressDTO = _mapper.Map<Dress, DressDTO>(addedDress);
            return addedDressDTO;
        }
        public async Task UpdateDress(int id, DressDTO updateDress)
        {
            Dress updeteDrs = _mapper.Map<DressDTO, Dress>(updateDress);
            if (updeteDrs.Price <= 0)
            {
                return;
            }
            if (_modelService.GetModelById(updeteDrs.ModelId) == null)
            {
                return;
            }

            await _dressRepository.UpdateDress(updeteDrs);
        }
        public async Task DeleteDress(DressDTO deleteDress)
        {
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
