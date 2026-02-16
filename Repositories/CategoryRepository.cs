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
        public async Task<Category> GetCategoryById(int id)
        {
            Category? categoryById = await _eventDressRentalContext.Categories.FindAsync(id);
            return categoryById;
        }
        public async Task<Category> AddCategory(Category category)
        {
            await _eventDressRentalContext.Categories.AddAsync(category);
            await _eventDressRentalContext.SaveChangesAsync();
            return category;
        }

        public async Task<bool> IsExistsCategoryById(int id)
        {
            return await _eventDressRentalContext.Categories.AnyAsync(u => u.Id == id);
        }

    }
}
