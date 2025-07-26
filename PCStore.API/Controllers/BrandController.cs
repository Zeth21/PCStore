using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using PCStore.Application.Features.CQRSDesignPattern.Commands.BrandCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.BrandQueries;
using PCStore.Application.Services.BrandService;

namespace PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController(IBrandService service) : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromBody] CreateBrandCommand request, CancellationToken cancellation = default) 
        {
            var result = await service.CreateBrand(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{brandId}")]
        public async Task<IActionResult> UpdateBrand(int brandId,[FromBody] string brandName, CancellationToken cancellation = default)
        {
            var request = new UpdateBrandCommand { BrandId = brandId, BrandName = brandName };
            var result = await service.UpdateBrand(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllBrands([FromQuery] GetAllBrandsQuery request, CancellationToken cancellation = default)
        {
            var result = await service.GetAllBrands(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }
    }
}
