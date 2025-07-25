using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.NotificationCommands
{
    public class RemoveNotificationByIdCommand : IRequest<Result>
    {
        public int NotificationId { get; set; }
        public required string UserId { get; set; }
    }
}
