using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCStore.Application.Features.CQRSDesignPattern.Commands.AttributeDefinitionCommands;
using PCStore.Application.Features.CQRSDesignPattern.Handlers.AttributeDefinitionHandlers.UpdateDTOs;
using PCStore.Application.Services.AttributeDefinitionService;

namespace PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributeDefinitionController(IAttributeDefinitionService attributeDefinitionService) : ControllerBase
    {
        private readonly IAttributeDefinitionService _attributeDefinitionService = attributeDefinitionService;

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateAttributeDefinition([FromBody] CreateAttributeDefinitionCommand request, CancellationToken can = default)
        {
            var result = await _attributeDefinitionService.CreateAttributeDefinition(request, can);
            return StatusCode(result.StatusCode, result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAttributeDefinition([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            var request = new RemoveAttributeDefinitionByIdCommand { Id = id };
            var result = await _attributeDefinitionService.RemoveAttributeDefinition(request, cancellationToken);
            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAttributeDefinition([FromRoute] int id, [FromBody] List<AttributeDefinitionProperties> properties, CancellationToken cancellationToken = default)
        {
            var request = new UpdateAttributeDefinitionCommand { Id = id, Properties = properties };
            var result = await _attributeDefinitionService.UpdateAttributeDefinition(request, cancellationToken);
            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
