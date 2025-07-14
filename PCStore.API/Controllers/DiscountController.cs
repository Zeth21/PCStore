using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCStore.Application.Features.CQRSDesignPattern.Commands.DiscountCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.DiscountQueries;
using PCStore.Application.Services.DiscountService;

namespace PCStore.API.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController(IDiscountService service) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateDiscount(CreateDiscountCommand request, CancellationToken cancellation = default)
        {
            var result = await service.CreateDiscount(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPatch("Deactive/{id}")]
        public async Task<IActionResult> DeactiveDiscount([FromRoute] int id, CancellationToken cancellation = default)
        {
            var request = new DeactiveDiscountCommand { DiscountId = id };
            var result = await service.DeactiveDiscount(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPatch("Active/{id}")]
        public async Task<IActionResult> ActiveDiscount([FromRoute] int id, CancellationToken cancellation = default)
        {
            var request = new ActiveDiscountCommand { DiscountId = id };
            var result = await service.ActiveDiscount(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateDiscount([FromBody] UpdateDiscountCommand request, CancellationToken cancellation = default)
        {
            var result = await service.UpdateDiscount(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDiscounts([FromQuery] GetAllDiscountsQuery request, CancellationToken cancellation = default)
        {
            var result = await service.GetAllDiscounts(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }
    }
}
