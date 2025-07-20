using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Queries.OrderStatusQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.OrderStatusResults;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.OrderStatusHandlers
{
    public class GetOrderStatusByOrderIdHandler(IMapper mapper, ProjectDbContext context) : IRequestHandler<GetOrderStatusByOrderIdQuery, TaskResult<GetOrderStatusByOrderIdResult>>
    {

        public async Task<TaskResult<GetOrderStatusByOrderIdResult>> Handle(GetOrderStatusByOrderIdQuery request, CancellationToken cancellationToken)
        {
            var lastOrderStatus = await context.OrderStatuses
                .Where(x => x.OrderId == request.OrderId)
                .OrderByDescending(x => x.StatusDate)
                .ProjectTo<GetOrderStatusByOrderIdResult>(mapper.ConfigurationProvider)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
            if (lastOrderStatus is null)
                return TaskResult<GetOrderStatusByOrderIdResult>.NotFound("No statuses has found!");
            return TaskResult<GetOrderStatusByOrderIdResult>.Success("Order status found successfully!",lastOrderStatus);
        }
    }
}
