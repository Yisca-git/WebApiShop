using Entities;

namespace Repositories
{
    public interface IDressRepository
    {
        Task<Dress> AddDress(Dress dress);
        Task DeleteDress(Dress dress);
        Task<Dress> GetDressById(int id);
        Task UpdateDress(Dress dress);
        Task<int> GetCountByModelIdAndSizeForDate(int id, string size, DateOnly date);
        Task<List<string>> GetSizesByModelId(int id);
        Task<bool> CheckDressByDate(int id, DateOnly date);
        Task<bool> IsExistsDressById(int id);
        Task<bool> IsDressAvailable(int id, DateOnly date);
        Task<int> GetPriceById(int id);
    }
}