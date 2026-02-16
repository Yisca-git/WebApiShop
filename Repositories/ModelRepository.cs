using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
namespace Repositories
{
    public class ModelRepository : IModelRepository
    {
        private readonly EventDressRentalContext _eventDressRentalContext;
        public ModelRepository(EventDressRentalContext eventDressRentalContext)
        {
            _eventDressRentalContext = eventDressRentalContext;
        }

        public async Task<(List<Model> items , int TotalCount)> GetModels(string? description, int? minPrice,
                       int? maxPrice, int[] categoriesId, string? color, int position = 1, int  skip = 8)
        {
            var query = _eventDressRentalContext.Models.Where(model =>
                model.IsActive == true
                &&(color == null ? (true) : (model.Color.Contains(color)))  
                &&(description == null ? (true) : (model.Description.Contains(description)))
                && ((minPrice == null) ? (true) : (model.BasePrice >= minPrice))
                && ((maxPrice == null) ? (true) : (model.BasePrice <= maxPrice))
                && ((categoriesId.Length == 0) ? (true) : (model.Categories.Any(c => categoriesId.Contains(c.Id)))))
                .OrderBy(model => model.BasePrice);

            Console.WriteLine(query.ToQueryString());
            List<Model> models = await query.Skip((position - 1) * skip)
            .Take(skip).Include(model => model.Categories).ToListAsync();
            var total = await query.CountAsync();
            return (models, total);

        }

        public async Task<Model> GetModelById(int id)
        {
            return await _eventDressRentalContext.Models.FirstOrDefaultAsync(o => o.Id == id && o.IsActive == true);
        }

        public async Task<Model> AddModel(Model model)
        {
            await _eventDressRentalContext.Models.AddAsync(model);
            foreach (var category in model.Categories)
            {
                _eventDressRentalContext.Entry(category).State = EntityState.Unchanged;
            }
            await _eventDressRentalContext.SaveChangesAsync();
            return model;
        }     
        public async Task DeleteModel(Model model)
        {
            _eventDressRentalContext.Models.Update(model);
            await _eventDressRentalContext.SaveChangesAsync();       
        }
        public async Task UpdateModel(Model model)
        {   
            _eventDressRentalContext.Models.Update(model);
            await _eventDressRentalContext.SaveChangesAsync();
        }

        public async Task<bool> IsExistsModelById(int id)
        {
            return await _eventDressRentalContext.Models.AnyAsync(m => m.Id == id && m.IsActive == true);
        }

    }
}
