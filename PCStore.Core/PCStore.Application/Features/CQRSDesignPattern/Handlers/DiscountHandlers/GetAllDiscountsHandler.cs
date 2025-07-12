using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PCStore.Application.Features.CQRSDesignPattern.Queries.DiscountQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.DiscountResults;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.DiscountHandlers
{
    public class GetAllDiscountsHandler(ProjectDbContext context, IMapper mapper) : IRequestHandler<GetAllDiscountsQuery, TaskListResult<GetAllDiscountsResult>>
    {
        public async Task<TaskListResult<GetAllDiscountsResult>> Handle(GetAllDiscountsQuery request, CancellationToken cancellationToken)
        {
            var query = context.Discounts.AsQueryable();
            if (!string.IsNullOrEmpty(request.DiscountName))
               query = query.Where(x => x.DiscountName.StartsWith(request.DiscountName));
            if (request.DiscountRate is not null)
               query = query.Where(x => x.DiscountRate == request.DiscountRate);
            query = query.Skip(request.PageSize * (request.PageNumber - 1));
            var discounts = await query.Take(request.PageSize).ToListAsync(cancellationToken);
            if (discounts.Count <= 0)
                return TaskListResult<GetAllDiscountsResult>.NotFound("No discounts found!");
            var result = mapper.Map<List<GetAllDiscountsResult>>(discounts);
            return TaskListResult<GetAllDiscountsResult>.Success("All discounts found successfully!", data: result);
        }
    }
}
