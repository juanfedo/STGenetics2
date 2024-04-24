using Application.DTO;
using AutoMapper;
using Infrastructure.Repositories;

namespace Application.Services
{
    public class FoodService : IFoodService
    {
        readonly IFoodRepository _foodRepository;
        readonly IMapper _mapper;

        public FoodService(IFoodRepository foodRepository, IMapper mapper)
        {
            _foodRepository = foodRepository;
            _mapper = mapper;
        }

        public async Task<List<FoodGetDTO>> GetAllFoodAsync(CancellationToken cancellationToken)
        {
            var food = await _foodRepository.GetAllFoodAsync(cancellationToken);
            return _mapper.Map<List<FoodGetDTO>>(food);
        }

        public async Task<List<FoodGetDTO>> GetFoodByTypeAsync(int typeId, CancellationToken cancellationToken)
        {
            var food = await _foodRepository.GetFoodByTypeAsync(typeId, cancellationToken);
            return _mapper.Map<List<FoodGetDTO>>(food);
        }
    }
}
