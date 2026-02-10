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
                (description == null ? (true) : (model.Description.Contains(description)))
                && ((minPrice == null) ? (true) : (model.BasePrice >= minPrice))
                && ((maxPrice == null) ? (true) : (model.BasePrice <= maxPrice))
                && ((categoriesId.Length == 0) ? (true) : (categoriesId.Contains(model.Categories)))
                .OrderBy(model => model.BasePrice);

            Console.WriteLine(query.ToQueryString());
            List<Model> models = await query.Skip((position - 1) * skip)
            .Take(skip).Include(model => model.Category).ToListAsync();
            var total = await query.CountAsync();
            return (models, total);

        }

        public async Task<Product> GetProductById(int id)
        {
            return await _eventDressRentalContext.Products.Include(p => p.Category).FirstOrDefaultAsync(o => o.Id == id);
        }



    }
}
