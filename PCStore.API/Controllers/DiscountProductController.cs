using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCStore.Application.Features.CQRSDesignPattern.Commands.DiscountProductCommand;
using PCStore.Application.Features.CQRSDesignPattern.Queries.DiscountProductQueries;
using PCStore.Application.Services.DiscountProductService;
using System.ComponentModel;

namespace PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountProductController(IDiscountProductService service) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateDiscountProducts([FromBody] CreateDiscountProductsCommand request, CancellationToken cancellation = default)
        {
            var result = await service.CreateDiscountProducts(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveDiscountProducts([FromBody] RemoveDiscountProductCommand request, CancellationToken cancellation = default)
        {
            var result = await service.RemoveDiscountProducts(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiscountProducts([FromRoute] int id, CancellationToken cancellation = default)
        {
            var request = new GetAllDiscountProductsQuery { DiscountId = id };
            var result = await service.GetAllDiscountProducts(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }
    }
}
