using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Queries.DiscountUsageQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.DiscountUsageResults;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.DiscountUsageHandlers
{
    public class GetAllDiscountUsageHandlers : IRequestHandler<GetAllDiscountUsagesQuery, TaskListResult<GetAllDiscountUsagesResult>>
    {
        private readonly ProjectDbContext _context;
        private readonly IMapper _mapper;
        public GetAllDiscountUsageHandlers(ProjectDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TaskListResult<GetAllDiscountUsagesResult>> Handle(GetAllDiscountUsagesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _context.Discounts
                    .Include(x => x.DiscountUsages)
                    .Where(x => x.DiscountUsages != null && x.DiscountUsages.Any()) 
                    .AsQueryable();

                if (!string.IsNullOrEmpty(request.DiscountName))
                    query = query.Where(x => x.DiscountName.StartsWith(request.DiscountName));

                if (request.DiscountIsPercentage is not null)
                    query = query.Where(x => x.DiscountIsPercentage == request.DiscountIsPercentage);

                if (request.DiscountStartDate is not null)
                    query = query.Where(x => x.DiscountStartDate <= request.DiscountStartDate);

                if (request.DiscountIsActive is not null)
                    query = query.Where(x => x.DiscountIsActive == request.DiscountIsActive);

                if (request.OrderBy is not null)
                {
                    if (request.OrderBy == false)
                        query = query.OrderBy(x => x.DiscountUsages!.Sum(du => du.DiscountTotal));
                    else
                        query = query.OrderByDescending(x => x.DiscountUsages!.Sum(du => du.DiscountTotal));
                }

                var discountUsages = await query
                    .AsNoTracking()
                    .Skip(request.PageSize * (request.PageNumber - 1))
                    .Take(request.PageSize)
                    .ProjectTo<GetAllDiscountUsagesResult>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return TaskListResult<GetAllDiscountUsagesResult>.Success("All discount usages found successfully!", discountUsages);
            }
            catch (Exception ex)
            {
                return TaskListResult<GetAllDiscountUsagesResult>.Fail("Something went wrong! " + ex.Message);
            }
        }

    }
}
