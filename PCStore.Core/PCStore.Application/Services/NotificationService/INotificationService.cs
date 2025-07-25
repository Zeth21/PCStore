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
    public interface INotificationService
    {
        Task<TaskResult<CreateNotificationResult>> CreateNotification(CreateNotificationCommand request, CancellationToken cancellation);
        Task<TaskResult<MarkNotificationAsSeenResult>> MarkNotificationAsSeen(MarkNotificationAsSeenCommand request, CancellationToken cancellation);
        Task<Result> RemoveNotification(RemoveNotificationByIdCommand request, CancellationToken cancellation);
        Task<TaskListResult<GetAllNotificationsByUserIdResult>> GetAllNotifications(GetAllNotificationsByUserIdQuery request, CancellationToken cancellation);
    }
}
