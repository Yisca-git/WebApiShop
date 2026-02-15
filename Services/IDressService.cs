using DTOs;

namespace Services
{
    public interface IDressService
    {
        Task<DressDTO> AddDress(NewDressDTO newDress);
        bool CheckDate(DateOnly date);
        bool CheckPrice(int price);
        Task DeleteDress(DressDTO deleteDress);
        Task<int> GetCountByModelIdAndSizeForDate(int id, string size, DateOnly date);
        Task<DressDTO> GetDressById(int id);
        Task<List<string>> GetSizesByModelId(int id);
        Task UpdateDress(int id, DressDTO updateDress);
    }
}