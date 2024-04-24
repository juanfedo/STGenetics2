using Domain.Models;

namespace Infrastructure.Repositories
{
    public interface IFoodRepository
    {
        Task<List<Food>> GetAllFoodAsync(CancellationToken cancellationToken);
        Task<List<Food>> GetFoodByTypeAsync(int typeId, CancellationToken cancellationToken);
    }
}