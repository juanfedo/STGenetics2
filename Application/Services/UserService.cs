using Application.DTO;
using AutoMapper;
using Domain.Models;
using Infrastructure.Repositories;

namespace Application.Services
{
    public class UserService : IUserService
    {
        readonly IUserRepository _userRepository;
        readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<int> CreateUserAsync(UserPostDTO userRequest, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(userRequest);
            var response = await _userRepository.CreateUserAsync(user, cancellationToken);
            return response;
        }

        public async Task<List<UserGetDTO>> GetUsersAsync(CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetUsersAsync(cancellationToken);
            return _mapper.Map<List<UserGetDTO>>(users);
        }
    }
}
