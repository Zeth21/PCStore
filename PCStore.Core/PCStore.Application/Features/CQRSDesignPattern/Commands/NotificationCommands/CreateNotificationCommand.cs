using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.NotificationResults;
using PCStore.Domain.Enum;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.NotificationCommands
{
    public class CreateNotificationCommand : IRequest<TaskResult<CreateNotificationResult>>
    {
        public NotifType NotificationType { get; set; }
        public required string NotificationTitle { get; set; }
        public required string NotificationContent { get; set; }
        public required string NotificationUserId { get; set; }
    }
}
