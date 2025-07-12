using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.BrandResults;

namespace PCStore.Application.Features.CQRSDesignPattern.Queries.BrandQueries
{
    public class GetBrandNameByProductIdQuery : IRequest<TaskResult<GetBrandNameByProductIdResult>>
    {
        public int BrandId { get; set; }
    }
}
