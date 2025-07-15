using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.OrderCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.OrderResults;
using PCStore.Application.Features.Helpers.Validators.CouponValidator;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.OrderHandlers
{
    public class CreateOrderHandler(ProjectDbContext context, IMapper mapper) : IRequestHandler<CreateOrderCommand, TaskResult<CreateOrderResult>>
    {
        public async Task<TaskResult<CreateOrderResult>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var checkAddress = await context.Addresses
                .AnyAsync(x => x.UserId == request.UserId && x.Id == request.OrderAddressId, cancellationToken);
            if (!checkAddress)
                return TaskResult<CreateOrderResult>.Fail("Invalid address for user!");
            var newOrder = mapper.Map<Order>(request);
            try 
            {
                await context.Orders.AddAsync(newOrder, cancellationToken);
                var saveResult = await context.SaveChangesAsync(cancellationToken);
                if (saveResult <= 0)
                    return TaskResult<CreateOrderResult>.Fail("Couldn't save the data!");
                var result = mapper.Map<CreateOrderResult>(newOrder);
                return TaskResult<CreateOrderResult>.Success("Order created successfully!", data: result);
            }
            catch(Exception ex) 
            {
                return TaskResult<CreateOrderResult>.Fail("Something went wrong! " + ex.Message);
            }
        }
    }
}
