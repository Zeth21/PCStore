using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCStore.Application.Features.CQRSDesignPattern.Commands.FollowedProductCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.FollowedProductsQuery;
using PCStore.Application.Services.FollowedProductsService;
using System.Security.Claims;

namespace PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowedProductController(IFollowedService followedService) : ControllerBase
    {
        private readonly IFollowedService _followedService = followedService;

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> CreateFollowing([FromBody] CreateFollowedProductCommand request, CancellationToken cancellation = default)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return Unauthorized();
            request.UserId = userId;
            var result = await _followedService.CreateFollowedProduct(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }

        [Authorize(Roles = "Customer")]
        [HttpDelete]
        public async Task<IActionResult> RemoveFollowing([FromBody] RemoveFollowedProductCommand request, CancellationToken cancellation = default)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return Unauthorized();
            request.UserId = userId;
            var result = await _followedService.RemoveFollowedProduct(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet]
        public async Task<IActionResult> GetFollowedProducts(CancellationToken cancellation = default)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return Unauthorized();
            var request = new GetFollowedProductsQuery { UserId = userId };
            var result = await _followedService.GetFollowedProducts(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }

    }
}
