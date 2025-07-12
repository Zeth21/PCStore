using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class AdminGetAllCouponsHandler(ProjectDbContext context, IMapper mapper) : IRequestHandler<AdminGetAllCouponsQuery, TaskListResult<AdminGetAllCouponsResult>>
    {
        public async Task<TaskListResult<AdminGetAllCouponsResult>> Handle(AdminGetAllCouponsQuery request, CancellationToken cancellationToken)
        {
            var query = context.Coupons.AsNoTracking().AsQueryable();

            if (request.IsActive is true)
                query = query.Where(x => x.CouponIsActive);

            if (!string.IsNullOrWhiteSpace(request.CouponValue))
                query = query.Where(x => x.CouponValue.ToString().StartsWith(request.CouponValue));

            if (!string.IsNullOrWhiteSpace(request.CouponCode))
                query = query.Where(x => x.CouponCode.Contains(request.CouponCode));

            var allCoupons = await query
                .Skip((request.PageNumber-1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            if (!allCoupons.Any())
                return TaskListResult<AdminGetAllCouponsResult>.NotFound("No coupons found!");

            var result = mapper.Map<List<AdminGetAllCouponsResult>>(allCoupons);
            return TaskListResult<AdminGetAllCouponsResult>.Success("All coupons found successfully!", data: result);
        }

    }
}
