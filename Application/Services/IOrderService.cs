using Application.DTO;

namespace Application.Services
{
    public interface IOrderService
    {
        Task<float> CreateOrderAsync(OrderPostDTO orderDTO, CancellationToken cancellationToken);
        Task<List<OrderGetDTO>> GetAllOrdersAsync(CancellationToken cancellationToken);
        Task DeleteOrderAsync(int id, CancellationToken cancellationToken);
        Task UpdateOrderAsync(OrderPatchDTO orderDTO, CancellationToken cancellationToken);
    }
}