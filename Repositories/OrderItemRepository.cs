using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
namespace Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly WebApiShopContext WebApiShopContext;
        public OrderItemRepository(WebApiShopContext _WebApiShopContext)
        {
            WebApiShopContext = _WebApiShopContext;
        }

        public async Task<IEnumerable<OrderItem>> AddOrderItems(IEnumerable<OrderItem> orderItems)
        {
            foreach (var orderItem in orderItems)
            {
                await WebApiShopContext.OrderItems.AddAsync(orderItem);
                await WebApiShopContext.SaveChangesAsync();
            }
            return orderItems;


        }
    }
}
