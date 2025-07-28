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
    public class GetAllCouponUsagesHandler : IRequestHandler<GetAllCouponUsagesQuery, TaskListResult<GetAllCouponUsagesResult>>
    {
        private readonly ProjectDbContext _context;
        private readonly IMapper _mapper;
        public GetAllCouponUsagesHandler(ProjectDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TaskListResult<GetAllCouponUsagesResult>> Handle(GetAllCouponUsagesQuery request, CancellationToken cancellationToken)
        {
            try 
            {
                var query = _context.Coupons
                        .Include(x => x.CouponUsages)
                        .Where(x => x.CouponUsages != null && x.CouponUsages.Any())
                        .AsNoTracking()
                        .AsQueryable();
                if (!string.IsNullOrEmpty(request.CouponCode))
                    query = query
                        .Where(x => x.CouponCode.StartsWith(request.CouponCode));
                if (request.CouponIsPercentage is not null)
                    query = query
                        .Where(x => x.CouponIsPercentage == request.CouponIsPercentage);
                if (request.CouponIsActive is not null)
                    query = query
                        .Where(x => x.CouponIsActive == request.CouponIsActive);
                if (request.CouponTargetType is not null)
                    query = query
                        .Where(x => x.CouponTargetType == request.CouponTargetType);
                if (request.OrderByTotalDiscount is not null)
                {
                    if (request.OrderByTotalDiscount == true)
                        query = query
                            .OrderBy(x => x.CouponUsages!.Sum(x => x.DiscountTotal));
                    else
                        query = query
                            .OrderByDescending(x => x.CouponUsages!.Sum(x => x.DiscountTotal));
                }
                var couponUsages = await query
                    .Skip(request.PageSize * (request.PageNumber - 1))
                    .Take(request.PageSize)
                    .ProjectTo<GetAllCouponUsagesResult>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
                return TaskListResult<GetAllCouponUsagesResult>.Success("All coupon usages has found successfully!", couponUsages);
            }
            catch(Exception ex) 
            {
                return TaskListResult<GetAllCouponUsagesResult>.Fail("Something went wrong! " + ex.Message);
            }
        }
    }
}
