using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.ProductQueries;
using PCStore.Application.Services.ProductService;
using PCStore.Application.Services.ProductService.ProductServiceCommands;
using System.Threading;

namespace PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService, IWebHostEnvironment env) : BaseController
    {

        private readonly IProductService _productService = productService;
        private readonly IWebHostEnvironment _env = env;
        //Products

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] GetAllProductsQuery request, CancellationToken cancellationToken = default)
        {
            var result = await _productService.GetAllProducts(request, cancellationToken);
            return StatusCode(result.StatusCode,result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductDetails(int id, CancellationToken can = default)
        {
            var result = await _productService.GetProductDetailsById(id, can);
            return StatusCode(result.StatusCode, result.Data);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] ServiceCreateProductCommand request, CancellationToken can = default)
        {
            request.Photos.WebRootPath = _env.WebRootPath;
            var result = await _productService.CreateProduct(request, can);
            return StatusCode(result.StatusCode, result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}/availability")]
        public async Task<IActionResult> UpdateAvailableProduct(int id, [FromBody] UpdateProductAvailabilityCommand req, CancellationToken cancellationToken = default)
        {
            req.ProductId = id;
            var result = await _productService.UpdateAvailableProduct(req, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateProduct([FromBody]UpdateProductCommand request,CancellationToken cancellationToken = default) 
        {
            var result = await _productService.UpdateProduct(request, cancellationToken);
            return StatusCode(result.StatusCode,result);
        }

        [HttpPut("type")]
        public async Task<IActionResult> UpdateProductType([FromBody]UpdateProductTypeCommand request, CancellationToken cancellationToken = default) 
        {
            var result = await _productService.UpdateProductType(request, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("discount")]
        public async Task<IActionResult> GetAllDiscountedProducts([FromQuery]GetDiscountedProductsQuery request, CancellationToken cancellation = default) 
        {
            var result = await _productService.GetAllDiscountedProducts(request, cancellation);
            return StatusCode(result.StatusCode, result.Data);
        }
        [HttpGet("new")]
        public async Task<IActionResult> GetNewProducts([FromQuery] GetNewProductsQuery request, CancellationToken cancellation = default)
        {
            var result = await _productService.GetNewProducts(request, cancellation);
            return StatusCode(result.StatusCode, result.Data);
        }
        [HttpGet("bestseller")]
        public async Task<IActionResult> GetBestSellingProducts([FromQuery] GetBestSellingProductsQuery request, CancellationToken cancellation = default)
        {
            var result = await _productService.GetBestSellingProducts(request, cancellation);
            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
