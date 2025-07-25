using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Queries.NotificationQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.NotificationResults;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.NotificationHandlers
{
    public class GetAllNotificationsByUserIdHandler(ProjectDbContext context, IMapper mapper) : IRequestHandler<GetAllNotificationsByUserIdQuery, TaskListResult<GetAllNotificationsByUserIdResult>>
    {
        public async Task<TaskListResult<GetAllNotificationsByUserIdResult>> Handle(GetAllNotificationsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var notifications = await context.Notifications
                .Where(x => x.NotificationUserId == request.UserId)
                .AsNoTracking()
                .OrderByDescending(x => x.NotificationDate)
                .Take(10)
                .ProjectTo<GetAllNotificationsByUserIdResult>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            if (notifications.Count <= 0)
                return TaskListResult<GetAllNotificationsByUserIdResult>.NotFound("No notifications has found!");
            return TaskListResult<GetAllNotificationsByUserIdResult>.Success("All notifications has found successfully!", notifications);
        }
    }
}
