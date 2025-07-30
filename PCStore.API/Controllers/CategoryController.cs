using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CategoryCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.CategoryQueries;
using PCStore.Application.Services.CategoryService;
using System.Text;

namespace PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService service) : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand request, CancellationToken cancellation = default) 
        {
            var result = await service.CreateCategory(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] int? parentCategoryId, CancellationToken cancellation = default) 
        {
            var request = new GetAllCategoriesQuery { ParentCategoryId = parentCategoryId };
            var result = await service.GetAllCategories(request, cancellation);
            return StatusCode(result.StatusCode, result.Data);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommand request, CancellationToken cancellation = default)
        {
            var result = await service.UpdateCategory(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }
    }
}
