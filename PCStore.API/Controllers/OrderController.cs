using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCStore.Application.Features.CQRSDesignPattern.Commands.OrderStatusCommands;
using PCStore.Application.Services.OrderService;
using PCStore.Application.Services.OrderService.Commands;
using System.Security.Claims;

namespace PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderService service) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] ServiceCreateOrderCommand request, CancellationToken cancellation = default) 
        {
            var result = await service.CreateOrder(request, cancellation);
            return StatusCode(result.StatusCode,result);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("{orderId}")]
        public async Task<IActionResult> UserGetOrder([FromRoute]int orderId, CancellationToken cancellation = default)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return Unauthorized();
            var request = new ServiceGetOrderDetailsByOrderIdCommand { OrderId = orderId, UserId = userId };
            var result = await service.UserGetOrderById(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("status")]
        public async Task<IActionResult> CreateOrderStatus([FromBody]CreateOrderStatusCommand request, CancellationToken cancellation = default) 
        {
            var result = await service.CreateOrderStatus(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }
    }
}
