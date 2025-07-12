using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CommentResults;

namespace PCStore.Application.Features.CQRSDesignPattern.Queries.CommentQueries
{
    public class GetQuestionsByProductIdQuery : IRequest<TaskListResult<GetQuestionsByProductIdResult>>
    {
        public int ProductId { get; set; }
        public int PageSize { get; set; } = 4;
        public int PageIndex { get; set; } = 0;
    }
}
