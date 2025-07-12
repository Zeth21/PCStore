using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductTypeAttributeCommands;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductTypeAttributeHandlers;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductTypeCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.AttributeDefinitionQueries;
using PCStore.Application.Services.TypeService;

namespace PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController(ITypeService typeService) : ControllerBase
    {
        private readonly ITypeService _typeService = typeService;

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateType([FromBody] CreateProductTypeCommand request, CancellationToken cancellationToken = default)
        {
            var result = await _typeService.CreateProductType(request, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }


        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateType([FromRoute] int id, [FromBody] string name, CancellationToken cancellationToken = default)
        {
            var request = new UpdateTypeCommand { Id = id, Name = name };
            var result = await _typeService.UpdateProductType(request, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveType([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            var request = new RemoveProductTypeCommand { Id = id };
            var result = await _typeService.RemoveProductType(request, cancellationToken);
            return StatusCode(result.StatusCode, result.Message);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTypeAttributes([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            var request = new GetTypeAttributesByIdQuery { TypeId = id };
            var result = await _typeService.GetTypeAttributesById(request, cancellationToken);
            return StatusCode(result.StatusCode, result.Data);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("Attributes")]
        public async Task<IActionResult> CreateTypeAttributes([FromBody] CreateTypeAttributeCommand request, CancellationToken cancellationToken = default)
        {
            var result = await _typeService.CreateTypeAttribute(request, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("Attributes/{id}")]
        public async Task<IActionResult> RemoveTypeAttributes([FromRoute] int id, CancellationToken cancellation = default)
        {
            var request = new RemoveTypeAttributeCommand { Id = id };
            var result = await _typeService.RemoveTypeAttributesById(request, cancellation);
            return StatusCode(result.StatusCode, result.Message);
        }
    }
}
