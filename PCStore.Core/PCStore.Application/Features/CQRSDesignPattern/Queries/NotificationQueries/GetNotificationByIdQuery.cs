using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.NotificationResults;

namespace PCStore.Application.Features.CQRSDesignPattern.Queries.NotificationQueries
{
    public class GetNotificationByIdQuery : IRequest<TaskResult<GetNotificationByIdResult>>
    {
        public int NotificationId { get; set; }
        public required string UserId { get; set; }
    }
}
