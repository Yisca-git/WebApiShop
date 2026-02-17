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
        private readonly IMapper _mapper;

        public DressService(IDressRepository dressRepository, IMapper mapper)
        {
            _dressRepository = dressRepository;
            _mapper = mapper;
        }
        //validations
        public bool CheckPrice(int price)
        {
            return price > 0;
        }
        public bool CheckDate(DateOnly date)
        {
            return date > DateOnly.FromDateTime(DateTime.Now);
        }

        //
        public async Task<DressDTO> GetDressById(int id)
        {
            Dress dress = await _dressRepository.GetDressById(id);
            DressDTO dressDTO = _mapper.Map<Dress, DressDTO>(dress);
            return dressDTO;
        }
        public async Task<DressDTO> AddDress(NewDressDTO newDress)
        {
            Dress dress = _mapper.Map<NewDressDTO, Dress>(newDress);
            dress.IsActive = true;
            Dress addedDress = await _dressRepository.AddDress(dress);
            DressDTO addedDressDTO = _mapper.Map<Dress, DressDTO>(addedDress);
            return addedDressDTO;
        }
        public async Task UpdateDress(int id, DressDTO updateDress)
        {
            Dress updeteDrs = _mapper.Map<DressDTO, Dress>(updateDress);
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
            return await _dressRepository.GetCountByModelIdAndSizeForDate(id, size, date);
        }
        public async Task<List<string>> GetSizesByModelId(int id)
        {
            return await _dressRepository.GetSizesByModelId(id);
        }
        public async Task<bool> CheckDressByDate(int id, DateOnly date)
        {
            return await _dressRepository.CheckDressByDate(id, date);
        }

        public async Task<bool> IsExistsDressById(int id)
        {
            return await _dressRepository.IsExistsDressById(id);
        }
        public async Task<bool> IsDressAvailable(int id, DateOnly date)
        {
            return await _dressRepository.IsDressAvailable(id, date);

        }
        public async Task<int> GetPriceById(int id)
        {
            return await _dressRepository.GetPriceById(id);

        }
    }
    }
