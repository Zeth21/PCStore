using PCStore.Domain.Enum;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.NotificationCommands
{
    public class CreateNotificationCommand
    {
        public NotifType NotificationType { get; set; }
        public DateTime NotificationDate { get; set; } = DateTime.Now;
        public bool NotificationStatus { get; set; } = false;
        public required string NotificationContent { get; set; }
        public required string NotificationUserId { get; set; }
    }
}
