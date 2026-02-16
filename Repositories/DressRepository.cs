using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
namespace Repositories
{
    public class DressRepository : IDressRepository
    {
        private readonly EventDressRentalContext _eventDressRentalContext;
        public DressRepository(EventDressRentalContext eventDressRentalContext)
        {
            _eventDressRentalContext = eventDressRentalContext;
        }
        public async Task<Dress> GetDressById(int id)
        {
            Dress? dressById = await _eventDressRentalContext.Dresses.FirstOrDefaultAsync(d => d.Id == id && d.IsActive == true);
            return dressById;
        }
        public async Task<Dress> AddDress(Dress dress)
        {
            await _eventDressRentalContext.Dresses.AddAsync(dress);
            await _eventDressRentalContext.SaveChangesAsync();
            return dress;
        }
        public async Task UpdateDress(Dress dress)
        {
            _eventDressRentalContext.Dresses.Update(dress);
            await _eventDressRentalContext.SaveChangesAsync();
        }
        public async Task DeleteDress(Dress dress)
        {
            _eventDressRentalContext.Dresses.Update(dress);
            await _eventDressRentalContext.SaveChangesAsync();
        }
        public async Task<int> GetCountByModelIdAndSizeForDate(int id, string size, DateOnly date)
        {
            var dressesCount = await _eventDressRentalContext.Dresses
                .Where(d => d.ModelId == id && d.Size == size && d.IsActive == true)
                .Include(d => d.OrderItems)
                    .ThenInclude(oi => oi.Order)
                .Where(d => !d.OrderItems.Any(oi =>
                    oi.Order.EventDate >= date.AddDays(-7) &&
                    oi.Order.EventDate <= date.AddDays(7)))
                .CountAsync();
            return dressesCount;
        }
        public async Task<List<string>> GetSizesByModelId(int id)
        {
            return await _eventDressRentalContext.Dresses
          .Where(m => m.ModelId == id && m.IsActive == true)
          .Select(d => d.Size).Distinct().ToListAsync();
        }
        public async Task<bool> CheckDressByDate(int id, DateOnly date)
        {
            var isDressAvailable = await _eventDressRentalContext.Dresses
                .Where(d => d.Id == id && d.IsActive == true)
                .Include(d => d.OrderItems)
                    .ThenInclude(oi => oi.Order)
                .Where(d => !d.OrderItems.Any(oi =>
                    oi.Order.EventDate >= date.AddDays(-7) &&
                    oi.Order.EventDate <= date.AddDays(7)))
                .AnyAsync();
            return isDressAvailable;
        }

    }
}
