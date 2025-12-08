using Entities;

namespace Repositories
{
    public interface IOrderItemRepository
    {
        Task<IEnumerable<OrderItem>> AddOrderItems(IEnumerable<OrderItem> orderItems);
    }
}