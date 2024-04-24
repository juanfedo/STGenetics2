using Application.DTO;

namespace Application.Services
{
    public interface IUserService
    {
        Task<int> CreateUserAsync(UserPostDTO userRequest, CancellationToken cancellationToken);
        Task<List<UserGetDTO>> GetUsersAsync(CancellationToken cancellationToken);
    }
}