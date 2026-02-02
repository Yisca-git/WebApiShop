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
        private readonly WebApiShopContext WebApiShopContext;
        public CategoryRepository(WebApiShopContext _WebApiShopContext)
        {
            WebApiShopContext = _WebApiShopContext;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await WebApiShopContext.Categories.ToListAsync();
        }



    }
}
