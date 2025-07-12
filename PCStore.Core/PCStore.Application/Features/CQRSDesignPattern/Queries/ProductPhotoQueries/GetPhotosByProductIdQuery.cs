using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductPhotoResults;

namespace PCStore.Application.Features.CQRSDesignPattern.Queries.ProductPhotoQueries
{
    public class GetPhotosByProductIdQuery : IRequest<TaskListResult<GetPhotosByProductIdResult>>
    {
        public int ProductId { get; set; }
    }
}
