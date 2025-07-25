using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PCStore.Application.Features.CQRSDesignPattern.Commands.NotificationCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.NotificationResults;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.NotificationHandlers
{
    public class CreateNotificationHandler(ProjectDbContext context, IMapper mapper) : IRequestHandler<CreateNotificationCommand, TaskResult<CreateNotificationResult>>
    {
        public async Task<TaskResult<CreateNotificationResult>> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var userName = await context.Users
                .Where(x => x.Id == request.NotificationUserId)
                .Select(x => x.UserName)
                .SingleOrDefaultAsync(cancellationToken);
            if (string.IsNullOrEmpty(userName))
                return TaskResult<CreateNotificationResult>.Fail("User not found!");
            var notification = mapper.Map<Notification>(request);
            try 
            {
                await context.Notifications.AddAsync(notification, cancellationToken);
                var task = await context.SaveChangesAsync(cancellationToken);
                if (task < 1)
                    return TaskResult<CreateNotificationResult>.Fail("Couldn't save the data!");
                var result = mapper.Map<CreateNotificationResult>(notification);
                result.UserName = userName;
                return TaskResult<CreateNotificationResult>.Success("Notification created successfully!",result);
            }
            catch(Exception ex) 
            {
                return TaskResult<CreateNotificationResult>.Fail("Something went wrong! " + ex.Message);
            }
        }
    }
}
