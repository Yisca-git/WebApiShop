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
        Task UpdateOrder(OrderDTO order);
        Task UpdateStatusOrder(OrderDTO order, int statusId);
    }
}