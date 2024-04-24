using Application.DTO;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace STGenetics2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        readonly IFoodService _foodService;

        public FoodController(IFoodService foodService)
        {
            _foodService = foodService;
        }

        /// <summary>
        /// Get all food
        /// </summary>
        /// <returns>The list of all food</returns>
        [HttpGet]
        public async Task<ActionResult<List<FoodGetDTO>>> Get(CancellationToken cancellationToken)
        {
            try
            {
                var users = await _foodService.GetAllFoodAsync(cancellationToken);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Get food by type
        /// </summary>
        /// <returns>The list of all food</returns>
        [HttpGet("ByType")]
        public async Task<ActionResult<List<FoodGetDTO>>> Get(int typeId, CancellationToken cancellationToken)
        {
            try
            {
                var users = await _foodService.GetFoodByTypeAsync(typeId, cancellationToken);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
