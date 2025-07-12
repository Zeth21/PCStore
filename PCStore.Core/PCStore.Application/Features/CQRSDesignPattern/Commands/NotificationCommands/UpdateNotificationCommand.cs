namespace PCStore.Application.Features.CQRSDesignPattern.Commands.NotificationCommands
{
    public class UpdateNotificationCommand
    {
        public int NotificationId { get; set; }
        public bool NotificationStatus { get; set; }
    }
}
