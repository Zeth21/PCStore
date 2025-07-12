using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductResults;

namespace PCStore.Application.Features.CQRSDesignPattern.Queries.ProductQueries
{
    public class GetProductDetailsByIdQuery : IRequest<TaskResult<GetProductInformationsResult>>
    {
        public int ProductId { get; set; }
    }
}
