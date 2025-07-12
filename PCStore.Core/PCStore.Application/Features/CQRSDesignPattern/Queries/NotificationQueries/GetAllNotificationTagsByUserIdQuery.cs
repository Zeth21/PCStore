namespace PCStore.Application.Features.CQRSDesignPattern.Queries.NotificationQueries
{
    public class GetAllNotificationTagsByUserIdQuery
    {
        public required string NotificationUserId { get; set; }
    }
}
