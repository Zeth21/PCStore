using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Queries.CouponUsageQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponUsageResults;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.CouponUsageHandlers
{
    public class GetCouponUsageByOrderIdHandler(ProjectDbContext context, IMapper mapper) : IRequestHandler<GetCouponUsageByOrderIdQuery, TaskResult<GetCouponUsageByOrderIdResult>>
    {
        public async Task<TaskResult<GetCouponUsageByOrderIdResult>> Handle(GetCouponUsageByOrderIdQuery request, CancellationToken cancellationToken)
        {
            var couponUsage = await context.CouponUsages
                .Where(x => x.CouponUsageOrderId == request.OrderId)
                .AsNoTracking()
                .ProjectTo<GetCouponUsageByOrderIdResult>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);
            if (couponUsage is null)
                return TaskResult<GetCouponUsageByOrderIdResult>.NotFound("No coupon usages found!");
            return TaskResult<GetCouponUsageByOrderIdResult>.Success("Coupon usage has found successfully!",couponUsage);
        }
    }
}
