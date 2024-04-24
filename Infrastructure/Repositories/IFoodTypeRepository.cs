using Domain.Models;

namespace Infrastructure.Repositories
{
    public interface IFoodTypeRepository
    {
        Task<FoodType> GetFoodTypeByIdAsync(int id, CancellationToken cancellationToken);
    }
}