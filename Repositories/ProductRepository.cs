using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly WebApiShopContext WebApiShopContext;
        public ProductRepository(WebApiShopContext _WebApiShopContext)
        {
            WebApiShopContext = _WebApiShopContext;
        }

        public async Task<IEnumerable<Product>> GetProducts(string? Description, int? minPrice,
                       int? maxPrice, int?[] categoriesId, int ? position, int  ?skip)
        {
            return await WebApiShopContext.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await WebApiShopContext.Products.FindAsync(id);
        }



    }
}
