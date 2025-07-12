using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCStore.Application.Features.CQRSDesignPattern.Commands.FollowedProductCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.FollowedProductsQuery;
using PCStore.Application.Services.FollowedProductsService;

namespace PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowedProductController(IFollowedService followedService) : ControllerBase
    {
        private readonly IFollowedService _followedService = followedService;

        [HttpPost]
        public async Task<IActionResult> CreateFollowing([FromBody] CreateFollowedProductCommand request, CancellationToken cancellation = default)
        {
            var result = await _followedService.CreateFollowedProduct(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveFollowing([FromBody] RemoveFollowedProductCommand request, CancellationToken cancellation = default)
        {
            var result = await _followedService.RemoveFollowedProduct(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetFollowedProducts(string userId, CancellationToken cancellation = default)
        {
            var request = new GetFollowedProductsQuery { UserId = userId };
            var result = await _followedService.GetFollowedProducts(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }

    }
}
