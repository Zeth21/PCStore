namespace PCStore.Application.Features.CQRSDesignPattern.Queries.NotificationQueries
{
    public class GetNewNotificationsCountByUserIdQuery
    {
        public required string NotificationUserId { get; set; }
    }
}
