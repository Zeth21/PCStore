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
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.NotificationHandlers
{
    public class GetNotificationByIdHandler(ProjectDbContext context, IMapper mapper) : IRequestHandler<GetNotificationByIdQuery, TaskResult<GetNotificationByIdResult>>
    {
        public async Task<TaskResult<GetNotificationByIdResult>> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
        {
            var notification = await context.Notifications
                .AsNoTracking()
                .Where(x => x.NotificationUserId == request.UserId && x.NotificationId == request.NotificationId)
                .ProjectTo<GetNotificationByIdResult>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
            if (notification is null)
                return TaskResult<GetNotificationByIdResult>.NotFound("Notification not found!");
            return TaskResult<GetNotificationByIdResult>.Success("Notification has found successfully!", notification);
        }
    }
}
