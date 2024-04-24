using Application.DTO;

namespace Application.Services
{
    public interface IFoodService
    {
        Task<List<FoodGetDTO>> GetAllFoodAsync(CancellationToken cancellationToken);
        Task<List<FoodGetDTO>> GetFoodByTypeAsync(int typeId, CancellationToken cancellationToken);
    }
}