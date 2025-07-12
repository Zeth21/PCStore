using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductPhotoCommands;
using PCStore.Application.Services.ProductPhotoService;

namespace PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductPhotoController(IPhotoService photoService, IWebHostEnvironment env) : ControllerBase
    {
        private readonly IPhotoService _photoService = photoService;
        private readonly IWebHostEnvironment _env = env;

        [HttpDelete]
        public async Task<IActionResult> RemovePhotos([FromBody]RemoveProductPhotoCommand request,CancellationToken cancellationToken = default) 
        {
            request.WebRootPath = _env.WebRootPath;
            var result = await _photoService.RemoveProductPhotos(request,cancellationToken);
            return StatusCode(result.StatusCode,result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePhotos([FromForm]UpdatePhotosCommand request, CancellationToken cancellationToken = default) 
        {
            request.WebRootPath = _env.WebRootPath;
            var result = await _photoService.UpdateProductPhotos(request, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
    }
}
