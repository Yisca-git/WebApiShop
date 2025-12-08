using Entities;

namespace Services
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItem>> AddOrderItems(IEnumerable<OrderItem> orderItems);
    }
}