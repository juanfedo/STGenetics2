using Application.DTO;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace STGenetics2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user">The details of the new user.</param>
        /// <returns>The id for the new user</returns>
        [HttpPost]
        public async Task<ActionResult> Post(UserPostDTO User, CancellationToken cancellationToken)
        {
            try
            {
                var newUserId = await _userService.CreateUserAsync(User, cancellationToken);
                return Ok(newUserId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>The list of all users</returns>
        [HttpGet]
        public async Task<ActionResult<List<UserGetDTO>>> Get(CancellationToken cancellationToken)
        {
            try
            {
                var users = await _userService.GetUsersAsync(cancellationToken);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
