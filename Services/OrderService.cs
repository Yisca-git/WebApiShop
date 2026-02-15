using AutoMapper;
using DTOs;
using Entities;
using Microsoft.Data.SqlClient;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public bool CheckOrder(NewOrderDTO order)
        {
            return order != null;
        }
        public bool CheckStatus(int status)
        {
            return status >= 1 && status <= 4;
        }
        public bool CheckDate(DateOnly date)
        {
            return date > DateOnly.FromDateTime(DateTime.Now);
        }
        public bool CheckDate(DateOnly OrderDate, DateOnly EventDate)
        {
            return OrderDate >= DateOnly.FromDateTime(DateTime.Now) && EventDate >= DateOnly.FromDateTime(DateTime.Now);
        }
        public bool CheckFinalPrice(NewOrderDTO newOrder)
        {
            Order order = _mapper.Map<NewOrderDTO, Order>(newOrder);
            int sum = 0;
            foreach (var item in order.OrderItems)
            {
                sum += item.Dress.Price;
            }
            if (order.FinalPrice != sum)
            {
                return false;
            }
            return true;
        }
        public async Task<OrderDTO> AddOrder(NewOrderDTO newOrder)
        {
            Order order = _mapper.Map<NewOrderDTO, Order>(newOrder);
            Order addedOrder = await _orderRepository.AddOrder(order);
            OrderDTO addedOrderDTO = _mapper.Map<Order, OrderDTO>(addedOrder);
            return addedOrderDTO;

        }
        public async Task<OrderDTO> GetOrderById(int id)
        {
            Order order = await _orderRepository.GetOrderById(id);
            if (order == null)
            {
                return null;
            }
            OrderDTO orderDTO = _mapper.Map<Order, OrderDTO>(order);
            return orderDTO;
        }
        public async Task<List<OrderDTO>> GetOrdersByUserId(int userId)
        {
            List<Order> orders = await _orderRepository.GetOrdersByUserId(userId);
            if (orders == null)
            {
                return null;
            }
            List<OrderDTO> orderDTOs = _mapper.Map<List<Order>, List<OrderDTO>>(orders);
            return orderDTOs;
        }
        public async Task<List<OrderDTO>> GetAllOrders()
        {
            List<Order> orders = await _orderRepository.GetAllOrders();
            List<OrderDTO> orderDTOs = _mapper.Map<List<Order>, List<OrderDTO>>(orders);
            return orderDTOs;
        }
        public async Task<List<OrderDTO>> GetOrderByDates(DateOnly date)
        {  
            List<Order> orders = await _orderRepository.GetOrdersByDate(date);
            List<OrderDTO> orderDTOs = _mapper.Map<List<Order>, List<OrderDTO>>(orders);
            return orderDTOs;

        }
        public async Task UpdateStatusOrder(OrderDTO upStsOrder, int statusId)
        {
            Order order = _mapper.Map<OrderDTO, Order>(upStsOrder);
            order.StatusId = statusId;
            await _orderRepository.UpdateStatusOrder(order);
        }
        public async Task UpdateOrder(OrderDTO updateOrder)
        {
            Order order = _mapper.Map<OrderDTO, Order>(updateOrder);
            await _orderRepository.UpdateOrder(order);
        }

    }
}
