using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.NotificationResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Queries.NotificationQueries
{
    public class GetNotSeenNotificationsCountQuery : IRequest<TaskResult<GetNotSeenNotificationsCountResult>>
    {
        public required string UserId { get; set; }
    }
}
