using Domain.Models;

namespace Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<int> CreateUserAsync(User user, CancellationToken cancellationToken);
        Task<List<User>> GetUsersAsync(CancellationToken cancellationToken);
    }
}