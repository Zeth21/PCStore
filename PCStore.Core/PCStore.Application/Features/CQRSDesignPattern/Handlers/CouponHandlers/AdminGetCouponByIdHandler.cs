using AutoMapper;
using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Queries.CouponQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponResults;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.CouponHandlers
{
    class AdminGetCouponByIdHandler(ProjectDbContext context, IMapper mapper) : IRequestHandler<AdminGetCouponByIdQuery, TaskResult<AdminGetCouponByIdResult>>
    {
        public async Task<TaskResult<AdminGetCouponByIdResult>> Handle(AdminGetCouponByIdQuery request, CancellationToken cancellationToken)
        {
            var coupon = await context.Coupons
                .FindAsync(request.Id,cancellationToken);
            if (coupon is null)
                return TaskResult<AdminGetCouponByIdResult>.Fail("Coupon not found!");
            var result = mapper.Map<AdminGetCouponByIdResult>(coupon);
            return TaskResult<AdminGetCouponByIdResult>.Success("Coupon found successfully!", data: result);
        }
    }
}
