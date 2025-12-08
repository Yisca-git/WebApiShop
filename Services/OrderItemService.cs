using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Repositories;

namespace Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemService(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;

        }

        public async Task<IEnumerable<OrderItem>> AddOrderItems(IEnumerable<OrderItem> orderItems)
        {
            return await _orderItemRepository.AddOrderItems(orderItems);
        }
    }
}
