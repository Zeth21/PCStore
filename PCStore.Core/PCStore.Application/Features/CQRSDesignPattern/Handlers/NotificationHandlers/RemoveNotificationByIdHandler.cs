using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.NotificationCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.NotificationHandlers
{
    public class RemoveNotificationByIdHandler(ProjectDbContext context) : IRequestHandler<RemoveNotificationByIdCommand, Result>
    {
        public async Task<Result> Handle(RemoveNotificationByIdCommand request, CancellationToken cancellationToken)
        {
            var notification = await context.Notifications
                .SingleOrDefaultAsync(x => x.NotificationId == request.NotificationId && x.NotificationUserId == request.UserId,cancellationToken);
            if (notification is null)
                return Result.NotFound("Notification not found!");
            try 
            {
                context.Notifications.Remove(notification);
                await context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
            catch(Exception ex) 
            {
                return Result.Fail("Something went wrong!" + ex.Message);
            }
        }
    }
}
