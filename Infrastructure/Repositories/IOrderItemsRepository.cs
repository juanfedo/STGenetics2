using Domain.Models;

namespace Infrastructure.Repositories
{
    public interface IOrderItemsRepository
    {
        Task CreateOrderItemsAsync(OrderItems orderItem, CancellationToken cancellationToken);
        Task<List<OrderItems>> GetOrderItemsByOrderIdAsync(int id, CancellationToken cancellationToken);
        Task<int> DeleteOrderItemsAsync(int orderId, CancellationToken cancellationToken);
        Task<int> UpdateOrderItemsAsync(OrderItems orderItem, CancellationToken cancellationToken);
        Task<List<Food>> GetOrderItemsByIdsAsync(string query, CancellationToken cancellationToken);
    }
}