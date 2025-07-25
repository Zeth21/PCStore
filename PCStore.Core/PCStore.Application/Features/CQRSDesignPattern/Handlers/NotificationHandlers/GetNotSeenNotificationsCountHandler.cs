using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Queries.NotificationQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.NotificationResults;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.NotificationHandlers
{
    public class GetNotSeenNotificationsCountHandler(ProjectDbContext context) : IRequestHandler<GetNotSeenNotificationsCountQuery, TaskResult<GetNotSeenNotificationsCountResult>>
    {
        public async Task<TaskResult<GetNotSeenNotificationsCountResult>> Handle(GetNotSeenNotificationsCountQuery request, CancellationToken cancellationToken)
        {
            var notifCount = await context.Notifications
                .Where(x => !x.NotificationStatus && x.NotificationUserId == request.UserId)
                .CountAsync(cancellationToken);
            var result = new GetNotSeenNotificationsCountResult { NotificationCount = notifCount };
            return TaskResult<GetNotSeenNotificationsCountResult>.Success("Success!", result);
        }
    }
}
