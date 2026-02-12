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
        public async Task<OrderDTO> AddOrder(NewOrderDTO newOrder)
        {
            if (newOrder == null)
                throw new ArgumentNullException(nameof(newOrder));
            if (newOrder.OrderDate < DateOnly.FromDateTime(DateTime.Now) || newOrder.EventDate < DateOnly.FromDateTime(DateTime.Now))
                throw new ArgumentException("Order date or event date cannot be in the past.");
            Order order = _mapper.Map<NewOrderDTO, Order>(newOrder);
            int sum = 0;
            foreach (var item in order.OrderItems)
            {
                sum += item.Dress.Price;
            }
            if (order.FinalPrice != sum)
            {
                throw new Exception("Final price does not match the sum of the dress prices.");
            }
            Order addedOrder = await _orderRepository.AddOrder(order);
            OrderDTO addedOrderDTO = _mapper.Map<Order, OrderDTO>(addedOrder);
            return addedOrderDTO;

        }
        public async Task<OrderDTO> GetOrderById(int id)
        {
            Order order = await _orderRepository.GetOrderById(id);
            if (order == null)
            {
                throw new Exception($"Order with id {id} not found.");
            }
            OrderDTO orderDTO = _mapper.Map<Order, OrderDTO>(order);
            return orderDTO;
        }
        public async Task<List<OrderDTO>> GetOrdersByUserId(int userId)
        {
            List<Order> orders = await _orderRepository.GetOrdersByUserId(userId);
            if (orders == null)
            {
                throw new Exception($"User with id {userId} not found.");
            }
            if (orders.Count == 0)
            {
                throw new Exception($"No orders found for user with id {userId}.");
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
            if (date <= DateOnly.FromDateTime(DateTime.Now))
            {
                throw new Exception("Date cannot be in the past.");
            }
            List<Order> orders = await _orderRepository.GetOrdersByDate(date);
            List<OrderDTO> orderDTOs = _mapper.Map<List<Order>, List<OrderDTO>>(orders);
            return orderDTOs;

        }
        public async Task UpdateStatusOrder(Order order, int statusId)
        {
            if(order == null)
            {
                throw new Exception(nameof(order));
            }
            if (statusId < 1 || statusId > 4)
            {
                throw new Exception("Invalid status id. Status id must be between 1 and 4.");
            }
            order.StatusId = statusId;
            await _orderRepository.UpdateStatusOrder(order);
        }
        public async Task UpdateOrder(Order order, int orderId)
        {
            if (order == null)
            {
                throw new Exception(nameof(order));
            }
            if (_orderRepository.GetOrderById(orderId) == null)
            {
                throw new Exception($"Order with id {orderId} not found.");
            }
            await _orderRepository.UpdateOrder(order);
        }

    }
}
