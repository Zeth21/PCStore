namespace PCStore.Application.Features.CQRSDesignPattern.Commands.NotificationCommands
{
    public class RemoveNotificationCommand
    {
        public int NotificationId { get; set; }

        public RemoveNotificationCommand(int notificationId)
        {
            NotificationId = notificationId;
        }
    }
}
