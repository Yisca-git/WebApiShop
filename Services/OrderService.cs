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
        private readonly IDressService _dressService;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository,IDressService dressService, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _dressService = dressService;
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
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            return OrderDate >= today &&
                   EventDate >= today &&
                   EventDate >= OrderDate;
        }
        public async Task<bool> CheckFinalPrice(NewOrderDTO newOrder)
        {
            Order postOrder = _mapper.Map<NewOrderDTO, Order>(newOrder);
            int sum = 0;
            foreach (var item in postOrder.OrderItems)
            {
                int dressSum = await _dressService.GetPriceById(item.DressId);
                sum += dressSum;
            }
            if (sum != postOrder.FinalPrice)
                return false;
            return true;
        }
        public async Task<bool> CheckFinalPrice(OrderDTO updateOrder)
        {
            Order postOrder = _mapper.Map<OrderDTO, Order>(updateOrder);
            int sum = 0;
            foreach (var item in postOrder.OrderItems)
            {
                int dressSum = await _dressService.GetPriceById(item.DressId);
                sum += dressSum;
            }
            if (sum != postOrder.FinalPrice)
                return false;
            return true;
        }
        public async Task<bool> CheckOrderItems(NewOrderDTO newOrder)
        {
            Order postOrder = _mapper.Map<NewOrderDTO, Order>(newOrder);
            foreach (var item in postOrder.OrderItems)
            {
                if (await _dressService.GetDressById(item.DressId) == null)
                {
                    return false;
                }
                bool isValid = await _dressService.IsDressAvailable(item.DressId, postOrder.EventDate);
                if (!isValid)
                    return false;
            }
            return true;
        }

        public async Task<bool> CheckOrderItems(OrderDTO updateOrder)
        {
            Order postOrder = _mapper.Map<OrderDTO, Order>(updateOrder);
            foreach (var item in postOrder.OrderItems)
            {
                if (await _dressService.GetDressById(item.DressId) == null)
                {
                    return false;
                }
                
            }
            return true;
        }

        public async Task<OrderDTO> AddOrder(NewOrderDTO newOrder)
        {
            Order order = _mapper.Map<NewOrderDTO, Order>(newOrder);
            order.StatusId = 1;
            Order addedOrder = await _orderRepository.AddOrder(order);
            OrderDTO addedOrderDTO = _mapper.Map<Order, OrderDTO>(addedOrder);
            return addedOrderDTO;

        }
        public async Task<OrderDTO> GetOrderById(int id)
        {
            Order order = await _orderRepository.GetOrderById(id);
            OrderDTO orderDTO = _mapper.Map<Order, OrderDTO>(order);
            return orderDTO;
        }
        public async Task<List<OrderDTO>> GetOrdersByUserId(int userId)
        {
            List<Order> orders = await _orderRepository.GetOrdersByUserId(userId);
            List<OrderDTO> orderDTOs = _mapper.Map<List<Order>, List<OrderDTO>>(orders);
            return orderDTOs;
        }
        public async Task<List<OrderDTO>> GetAllOrders()
        {
            List<Order> orders = await _orderRepository.GetAllOrders();
            List<OrderDTO> orderDTOs = _mapper.Map<List<Order>, List<OrderDTO>>(orders);
            return orderDTOs;
        }
        public async Task<List<OrderDTO>> GetUnpackedOrdersUntilDate(DateOnly date)
        {
            List<Order> orders = await _orderRepository.GetUnpackedOrdersUntilDate(date);
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
        public async Task<bool> IsExistsOrderById(int id)
        {
            return await _orderRepository.IsExistsOrderById(id);
        }

        }
    }
