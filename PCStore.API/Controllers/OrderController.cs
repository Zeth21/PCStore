using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCStore.Application.Services.OrderService;
using PCStore.Application.Services.OrderService.Commands;

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
    }
}
