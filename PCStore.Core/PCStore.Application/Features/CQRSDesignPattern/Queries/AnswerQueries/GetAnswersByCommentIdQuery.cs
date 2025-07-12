using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.AnswerResults;

namespace PCStore.Application.Features.CQRSDesignPattern.Queries.AnswerQueries
{
    public class GetAnswersByCommentIdQuery : IRequest<TaskListResult<GetAnswersByCommentIdResult>>
    {
        public int CommentId { get; set; }
    }
}
