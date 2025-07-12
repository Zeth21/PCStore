using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductAttributeCommands;
using PCStore.Application.Services.ProductAttributeService;

namespace PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAttributeController(IProductAttributeService attributeService) : ControllerBase
    {
        private readonly IProductAttributeService _attributeService = attributeService;

        [HttpPut]
        public async Task<IActionResult> UpdateProductAttributes([FromBody]BulkUpdateProductAttributeCommand request, CancellationToken cancellationToken = default) 
        {
            var result = await _attributeService.UpdateProductAttributes(request, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
    }
}
