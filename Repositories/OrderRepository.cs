using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly WebApiShopContext WebApiShopContext;
        public OrderRepository(WebApiShopContext _WebApiShopContext)
        {
            WebApiShopContext = _WebApiShopContext;
        }

        public async Task<Order> AddOrder(Order order)
        {
            await WebApiShopContext.Orders.AddAsync(order);
            await WebApiShopContext.SaveChangesAsync();
            return order;
        }
        public async Task<Order> GetOrderById(int id)
        {
            return await WebApiShopContext.Orders.FindAsync(id);
        }
    }
}
