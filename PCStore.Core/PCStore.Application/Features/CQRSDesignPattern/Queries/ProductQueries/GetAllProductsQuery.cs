using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductResults;

namespace PCStore.Application.Features.CQRSDesignPattern.Queries.ProductQueries
{
    public class GetAllProductsQuery : IRequest<TaskListResult<GetAllProductsResult>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;
        public string? Name { get; set; }
        public string? CategoryName { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public bool? OrderByDesc { get; set; }
        public string? SortBy { get; set; }
    }
}
