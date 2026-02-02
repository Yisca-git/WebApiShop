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

        public async Task<(List<Product> items , int TotalCount)> GetProducts(string? description, int? minPrice,
                       int? maxPrice, int[] categoriesId, int position = 1, int  skip = 8)
        {
            var query = WebApiShopContext.Products.Where(product =>
                (description == null ? (true) : (product.Description.Contains(description)))
                && ((minPrice == null) ? (true) : (product.Price >= minPrice))
                && ((maxPrice == null) ? (true) : (product.Price <= maxPrice))
                && ((categoriesId.Length == 0) ? (true) : (categoriesId.Contains(product.CategoryId))))
                .OrderBy(product => product.Price);

            Console.WriteLine(query.ToQueryString());
            List<Product> products = await query.Skip((position - 1) * skip)
            .Take(skip).Include(product => product.Category).ToListAsync();
            var total = await query.CountAsync();
            return (products, total);

        }

        public async Task<Product> GetProductById(int id)
        {
            return await WebApiShopContext.Products.Include(p => p.Category).FirstOrDefaultAsync(o => o.Id == id);
        }



    }
}
