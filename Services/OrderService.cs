using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entities;
using Entities.DTO;
using Repositories;
namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;

        }
        public async Task<OrderDTO> AddOrder(Order order)
        {
            Order _order = await _orderRepository.AddOrder(order);
            OrderDTO orderDTO = _mapper.Map<Order, OrderDTO>(_order);
            return orderDTO;
            
        }
        public async Task<OrderDTO> GetOrderById(int id)
        {
            Order order = await _orderRepository.GetOrderById(id);
            OrderDTO orderDTO = _mapper.Map<Order, OrderDTO>(order);
            return orderDTO;
        }

    }
}
