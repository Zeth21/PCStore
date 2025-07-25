using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.NotificationCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.NotificationResults;
using PCStore.Persistence.Context;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.NotificationHandlers
{
    public class MarkNotificationAsSeenHandler(ProjectDbContext context) : IRequestHandler<MarkNotificationAsSeenCommand, TaskResult<MarkNotificationAsSeenResult>>
    {
        public async Task<TaskResult<MarkNotificationAsSeenResult>> Handle(MarkNotificationAsSeenCommand request, CancellationToken cancellationToken)
        {
            var notification = await context.Notifications
                .SingleOrDefaultAsync(x => x.NotificationId == request.NotificationId && x.NotificationUserId == request.UserId);
            if (notification is null)
                return TaskResult<MarkNotificationAsSeenResult>.NotFound("Notification not found!");
            notification.NotificationStatus = true;
            await context.SaveChangesAsync(cancellationToken);
            return TaskResult<MarkNotificationAsSeenResult>.Success("Notification marked successfully!", new MarkNotificationAsSeenResult { NotificationId = notification.NotificationId });
        }
    }
}
