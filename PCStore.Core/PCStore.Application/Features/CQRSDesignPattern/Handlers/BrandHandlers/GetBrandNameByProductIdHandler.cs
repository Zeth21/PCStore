using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Queries.BrandQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.BrandResults;
using PCStore.Persistence.Context;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.BrandHandlers
{
    public class GetBrandNameByProductIdHandler(ProjectDbContext projectDbContext) : IRequestHandler<GetBrandNameByProductIdQuery, TaskResult<GetBrandNameByProductIdResult>>
    {
        private readonly ProjectDbContext _projectDbContext = projectDbContext;
        public async Task<TaskResult<GetBrandNameByProductIdResult>> Handle(GetBrandNameByProductIdQuery request, CancellationToken cancellationToken)
        {
            var brand = await _projectDbContext.Brands.Where(x => x.BrandId == request.BrandId).FirstOrDefaultAsync(cancellationToken);
            if (brand is null)
                return TaskResult<GetBrandNameByProductIdResult>.NotFound("Brand not found!");
            var res = new GetBrandNameByProductIdResult { BrandName = brand.BrandName };
            return TaskResult<GetBrandNameByProductIdResult>.Success(message: "Brand is found successfully", data: res);
        }
    }
}
