using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.OrderStatusCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.OrderStatusResults;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.OrderStatusHandlers
{
    public class CreateOrderStatusHandler(ProjectDbContext context) : IRequestHandler<CreateOrderStatusCommand, TaskResult<CreateOrderStatusResult>>
    {
        public async Task<TaskResult<CreateOrderStatusResult>> Handle(CreateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var statusName = await context.StatusNames.Where(x => x.StatusNameId == request.StatusNameId).Select(x => x.StatusNameString).SingleOrDefaultAsync();
            if (statusName is null)
                return TaskResult<CreateOrderStatusResult>.Fail("Invalid status name!");
            var newRecord = new OrderStatus 
            { 
                OrderId = request.OrderId,
                StatusDate = request.StatusDate, 
                StatusNameId = request.StatusNameId 
            };
            try 
            {
                await context.OrderStatuses.AddAsync(newRecord, cancellationToken);
                var resultTask = await context.SaveChangesAsync(cancellationToken);
                if (resultTask <= 0)
                    return TaskResult<CreateOrderStatusResult>.Fail("Couldn't save the data to the database!");
                var result = new CreateOrderStatusResult
                {
                    OrderId = newRecord.OrderId,
                    StatusDate = newRecord.StatusDate,
                    StatusId = newRecord.StatusId,
                    StatusName = statusName,
                };
                return TaskResult<CreateOrderStatusResult>.Success("Order status created successfully!",data:result);
            }
            catch(Exception ex) 
            {
                return TaskResult<CreateOrderStatusResult>.Fail("Something went wrong! " + ex.Message);
            }
        }
    }
}
