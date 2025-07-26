using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Queries.BrandQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.BrandResults;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.BrandHandlers
{
    public class GetAllBrandsHandler(ProjectDbContext context, IMapper mapper) : IRequestHandler<GetAllBrandsQuery, TaskListResult<GetAllBrandsResult>>
    {
        public async Task<TaskListResult<GetAllBrandsResult>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            try 
            {
                var query = context.Brands.AsQueryable();
                if (!string.IsNullOrEmpty(request.BrandName))
                    query = query.Where(x => x.BrandName!.StartsWith(request.BrandName));
                var brands = await query.Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ProjectTo<GetAllBrandsResult>(mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
                if (brands.Count <= 0)
                    return TaskListResult<GetAllBrandsResult>.NotFound("No brands has found!");
                return TaskListResult<GetAllBrandsResult>.Success("All brands has found successfully!", brands);
            }
            catch(Exception ex) 
            {
                return TaskListResult<GetAllBrandsResult>.Fail("Something went wrong! " + ex.Message);
            }
        }
    }
}
