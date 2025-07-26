using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Queries.CategoryQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CategoryResults;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.CategoryHandlers
{
    public class GetAllCategoriesHandler(ProjectDbContext context, IMapper mapper) : IRequestHandler<GetAllCategoriesQuery, TaskResult<GetAllCategoriesResult>>
    {
        public async Task<TaskResult<GetAllCategoriesResult>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            try 
            {
                var query = context.Categories.AsQueryable();
                if (request.ParentCategoryId is null)
                {
                    query = query.Where(x => x.ParentCategoryId == null);
                }
                else
                {
                    query = query.Where(x => x.ParentCategoryId == request.ParentCategoryId);
                }
                var categories = await query
                    .AsNoTracking()
                    .ProjectTo<CategoryListItem>(mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
                if (categories.Count <= 0)
                    return TaskResult<GetAllCategoriesResult>.NotFound("No categories has found!");
                var result = new GetAllCategoriesResult
                {
                    ParentCategoryId = request.ParentCategoryId,
                    Categories = categories
                };
                return TaskResult<GetAllCategoriesResult>.Success("All categories has found successfully!", result);
            }
            catch(Exception ex) 
            {
                return TaskResult<GetAllCategoriesResult>.Fail("Something went wrong! " + ex.Message);
            }
        }
    }
}
