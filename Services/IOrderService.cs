using DTOs;

namespace Services
{
    public interface IOrderService
    {
        Task<OrderDTO> AddOrder(NewOrderDTO newOrder);
        bool CheckDate(DateOnly date);
        bool CheckDate(DateOnly OrderDate, DateOnly EventDate);
        bool CheckFinalPrice(NewOrderDTO newOrder);
        bool CheckFinalPrice(OrderDTO updateOrder);
        bool CheckOrder(NewOrderDTO order);
        bool CheckStatus(int status);
        Task<bool> CheckOrderItems(NewOrderDTO newOrder);
        Task<bool> CheckOrderItems(OrderDTO newOrder);
        Task<List<OrderDTO>> GetAllOrders();
        Task<OrderDTO> GetOrderById(int id);
        Task<List<OrderDTO>> GetOrdersByUserId(int userId);
        Task<List<OrderDTO>> GetUnpackedOrdersUntilDate(DateOnly date);
        Task UpdateOrder(OrderDTO updateOrder);
        Task UpdateStatusOrder(OrderDTO upStsOrder, int statusId);
    }
}