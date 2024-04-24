using Application.DTO;
using Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace STGenetics2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Get all orders
        /// </summary>
        /// <returns>The list of all orders</returns>
        [HttpGet]
        public async Task<ActionResult<List<OrderGetDTO>>> Get(CancellationToken cancellationToken)
        {
            try
            {
                var users = await _orderService.GetAllOrdersAsync(cancellationToken);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Create a new order
        /// </summary>
        /// <param name="orderPostDTO">Order DTO</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post(OrderPostDTO orderPostDTO, CancellationToken cancellationToken)
        {
            try
            {
                var price = await _orderService.CreateOrderAsync(orderPostDTO, cancellationToken);
                return Ok($"Total price: {price}" );
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Delete a order
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _orderService.DeleteOrderAsync(id, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Update a order
        /// </summary>
        /// <param name="orderPost"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<ActionResult> Patch(OrderPatchDTO orderPost, CancellationToken cancellationToken)
        {
            try
            {
                await _orderService.UpdateOrderAsync(orderPost, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
