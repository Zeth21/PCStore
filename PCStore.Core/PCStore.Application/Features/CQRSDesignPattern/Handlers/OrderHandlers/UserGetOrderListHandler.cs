using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Queries.OrderQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.OrderResults;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.OrderHandlers
{
    public class UserGetOrderListHandler(ProjectDbContext context, IMapper mapper) : IRequestHandler<UserGetOrderListQuery, TaskListResult<UserGetOrderListResult>>
    {
        public async Task<TaskListResult<UserGetOrderListResult>> Handle(UserGetOrderListQuery request, CancellationToken cancellationToken)
        {
            var orders = await context.Orders
                .Where(x => x.OrderUserId == request.UserId)
                .AsNoTracking()
                .ProjectTo<UserGetOrderListResult>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            if (orders.Count <= 0)
                return TaskListResult<UserGetOrderListResult>.NotFound("No orders found!");
            return TaskListResult<UserGetOrderListResult>.Success("All orders has found successfully!", orders);
        }
    }
}
