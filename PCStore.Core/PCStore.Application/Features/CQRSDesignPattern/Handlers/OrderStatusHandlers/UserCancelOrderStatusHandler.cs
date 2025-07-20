using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.OrderStatusCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.OrderStatusHandlers
{
    public class UserCancelOrderStatusHandler(ProjectDbContext context) : IRequestHandler<UserCancelOrderStatusCommand, Result>
    {
        public async Task<Result> Handle(UserCancelOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await context.Orders
                .Where(x => x.OrderId == request.OrderId
                && x.OrderUserId == request.UserId
                && x.OrderIsActive)
                .AnyAsync(cancellationToken);
            if (!order)
                return Result.Fail("Order not found!");
            var newStatus = new OrderStatus
            {
                OrderId = request.OrderId,
                StatusDate = DateTime.Now,
                StatusNameId = 5
            };
            try 
            {
                await context.OrderStatuses.AddAsync(newStatus,cancellationToken);
                var task = await context.SaveChangesAsync(cancellationToken);
                if (task <= 0)
                    return Result.Fail("Couldn't save the data!");
                return Result.Success("Order cancelled successfully!");
            }
            catch(Exception ex) 
            {
                return Result.Fail("Something went wrong! " + ex.Message);
            }
        }
    }
}
