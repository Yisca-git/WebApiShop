using Entities;

namespace Repositories
{
    public interface IOrderRepository
    {
        Task<Order> AddOrder(Order order);
        Task<Order> GetOrderById(int id);
        Task<List<Order>> GetOrdersByUserId(int userId);
        Task<List<Order>> GetAllOrders();
        Task<List<Order>> GetOrderByDates(DateOnly date);
    }
}