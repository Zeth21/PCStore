using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CreateShoppingCartItemsCommand;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ShoppingCartItemsCommand;
using PCStore.Application.Features.CQRSDesignPattern.Queries.ShoppingCartItemsQueries;
using PCStore.Application.Services.ShoppingCartItemService;
using System.Security.Claims;

namespace PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController(IShoppingCartService cartService) : ControllerBase
    {
        private readonly IShoppingCartService _cartService = cartService;
        [HttpPost]
        public async Task<IActionResult> CreateShopCartItem([FromBody] CreateShopCartItemCommand request, CancellationToken cancellation = default) 
        {
            var result = await _cartService.CreateShopCartItem(request,cancellation);
            return StatusCode(result.StatusCode, result);
        }

        [Authorize(Roles ="Customer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveShopCartItem([FromRoute] int id, CancellationToken cancellation = default) 
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
                return Unauthorized();
            var request = new RemoveShopCartItemCommand { Id = id, UserId = userId };
            var result = await _cartService.RemoveShopCartItem(request, cancellation);
            return StatusCode(result.StatusCode,result);
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpGet]
        public async Task<IActionResult> GetShopCartItems([FromQuery]int? couponId,CancellationToken cancellation = default) 
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
                return Unauthorized();
            var request = new GetShopCartItemsQuery { UserId = userId, CouponId = couponId};
            var result = await _cartService.GetShopCartItems(request,cancellation);
            return StatusCode(result.StatusCode,result);
        }
     }
}
