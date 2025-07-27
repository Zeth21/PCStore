using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCStore.Application.Features.CQRSDesignPattern.Commands.AddressCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.AddressQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results.AddressResults;
using PCStore.Application.Services.AddressService;
using System.Security.Claims;

namespace PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController(IAddressService service) : ControllerBase
    {
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] CreateAddressCommand request, CancellationToken cancellation = default) 
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return Unauthorized();
            request.UserId = userId;
            var result = await service.CreateAddress(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }

        [Authorize(Roles = "Customer")]
        [HttpDelete("{addressId}")]
        public async Task<IActionResult> RemoveAddress(int addressId, CancellationToken cancellation = default) 
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return Unauthorized();
            var request = new RemoveAddressCommand { Id = addressId, UserId = userId };
            var result = await service.RemoveAddress(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }

        [Authorize(Roles = "Customer")]
        [HttpPut]
        public async Task<IActionResult> UpdateAddress([FromBody] UpdateAddressCommand request, CancellationToken cancellation = default)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return Unauthorized();
            request.UserId = userId;
            var result = await service.UpdateAddress(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet]
        public async Task<IActionResult> GetAllAddresses(CancellationToken cancellation = default)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return Unauthorized();
            var request = new GetAllAddressesQuery { UserId = userId };
            var result = await service.GetAllAddresses(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }
    }
}
