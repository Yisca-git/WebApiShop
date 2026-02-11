using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EventDressRentalContext _eventDressRentalContext;
        public CategoryRepository(EventDressRentalContext eventDressRentalContext)
        {
            _eventDressRentalContext = eventDressRentalContext;
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _eventDressRentalContext.Categories.ToListAsync();
        }

        public async Task<Category> AddCategory(Category category)
        {
            await _eventDressRentalContext.Categories.AddAsync(category);
            await _eventDressRentalContext.SaveChangesAsync();
            return category;
        }



    }
}
