using DTOs;
using Entities;

namespace Services
{
    public interface IOrderService
    {
        Task<OrderDTO> AddOrder(NewOrderDTO NewOrder);
        Task<List<OrderDTO>> GetAllOrders();
        Task<List<OrderDTO>> GetOrderByDates(DateOnly date);
        Task<OrderDTO> GetOrderById(int id);
        Task<List<OrderDTO>> GetOrdersByUserId(int userId);
        Task UpdateOrder(Order order, int orderId);
        Task UpdateStatusOrder(Order order, int statusId);
    }
}