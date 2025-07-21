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
    public class ListGetOrderStatusByOrderIdHandler(ProjectDbContext context, IMapper mapper) : IRequestHandler<ListGetOrderStatusByOrderIdQuery, TaskListResult<ListGetOrderStatusByOrderIdResult>>
    {
        public async Task<TaskListResult<ListGetOrderStatusByOrderIdResult>> Handle(ListGetOrderStatusByOrderIdQuery request, CancellationToken cancellationToken)
        {
            var orderStatus = await context.OrderStatuses
                .Where(x => x.OrderId == request.OrderId)
                .OrderByDescending(x => x.StatusDate)
                .AsNoTracking()
                .ProjectTo<ListGetOrderStatusByOrderIdResult>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            if (orderStatus.Count <= 0)
                return TaskListResult<ListGetOrderStatusByOrderIdResult>.NotFound("No status has found!");
            return TaskListResult<ListGetOrderStatusByOrderIdResult>.Success("All status has found successfully!", orderStatus);
        }
    }
}
