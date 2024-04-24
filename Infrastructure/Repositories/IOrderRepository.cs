using Domain.Models;

namespace Infrastructure.Repositories
{
    public interface IOrderRepository
    {
        Task<int> CreateOrderAsync(Order order, CancellationToken cancellationToken);
        Task<int> DeleteOrderAsync(int id, CancellationToken cancellationToken);
        Task<List<Order>> GetAllOrdersAsync(CancellationToken cancellationToken);
        Task<List<Order>> GetOrderByIdAsync(int id, CancellationToken cancellationToken);
        Task<int> UpdateOrderAsync(Order order, CancellationToken cancellationToken);
    }
}