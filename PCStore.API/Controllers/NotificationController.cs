using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCStore.Application.Features.CQRSDesignPattern.Commands.NotificationCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.NotificationQueries;
using PCStore.Application.Services.NotificationService;
using System.Security.Claims;

namespace PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController(INotificationService service) : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationCommand request, CancellationToken cancellation = default)
        {
            var result = await service.CreateNotification(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }

        [Authorize(Roles = "Customer")]
        [HttpPatch("{notificationId}")]
        public async Task<IActionResult> MarkNotificationAsSeen(int notificationId, CancellationToken cancellation = default) 
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return Unauthorized();
            var request = new MarkNotificationAsSeenCommand { NotificationId = notificationId, UserId = userId };
            var result = await service.MarkNotificationAsSeen(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }

        [Authorize(Roles = "Customer")]
        [HttpDelete("{notificationId}")]
        public async Task<IActionResult> RemoveNotification(int notificationId, CancellationToken cancellation = default)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return Unauthorized();
            var request = new RemoveNotificationByIdCommand { NotificationId = notificationId, UserId = userId };
            var result = await service.RemoveNotification(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet]
        public async Task<IActionResult> GetAllNotifications(CancellationToken cancellation = default) 
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return Unauthorized();
            var request = new GetAllNotificationsByUserIdQuery { UserId = userId };
            var result = await service.GetAllNotifications(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }
    }
}
