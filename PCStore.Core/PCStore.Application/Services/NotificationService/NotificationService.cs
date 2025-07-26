using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Commands.NotificationCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.NotificationQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.NotificationResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Services.NotificationService
{
    public class NotificationService(IMediator mediator) : INotificationService
    {
        public async Task<TaskResult<CreateNotificationResult>> CreateNotification(CreateNotificationCommand request, CancellationToken cancellation)
        {
            var result = await mediator.Send(request, cancellation);
            return result;
        }

        public async Task<TaskListResult<GetAllNotificationsByUserIdResult>> GetAllNotifications(GetAllNotificationsByUserIdQuery request, CancellationToken cancellation)
        {
            var result = await mediator.Send(request, cancellation);
            return result;
        }

        public async Task<TaskResult<GetNotificationByIdResult>> GetNotificationById(GetNotificationByIdQuery request, CancellationToken cancellation)
        {
            var result = await mediator.Send(request, cancellation);
            return result;
        }

        public async Task<TaskResult<MarkNotificationAsSeenResult>> MarkNotificationAsSeen(MarkNotificationAsSeenCommand request, CancellationToken cancellation)
        {
            var result = await mediator.Send(request, cancellation);
            return result;
        }

        public async Task<Result> RemoveNotification(RemoveNotificationByIdCommand request, CancellationToken cancellation)
        {
            var result = await mediator.Send(request, cancellation);
            return result;
        }
    }
}
