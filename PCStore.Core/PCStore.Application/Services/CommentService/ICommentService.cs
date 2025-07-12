using PCStore.Application.Features.CQRSDesignPattern.Commands.CommentCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.CommentQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CommentResults;

namespace PCStore.Application.Services.CommentService
{
    public interface ICommentService
    {
        public Task<TaskListResult<GetQuestionsByProductIdResult>> GetQuestions(GetQuestionsByProductIdQuery request, CancellationToken cancellationToken);
        public Task<TaskListResult<GetCommentsByProductIdResult>> GetComments(GetCommentsByProductIdQuery request, CancellationToken cancellationToken);
        public Task<TaskResult<CreateCommentResult>> CreateComment(CreateCommentCommand request, CancellationToken cancellation);
        public Task<Result> RemoveComment(RemoveCommentCommand request, CancellationToken cancellation);
        public Task<TaskResult<UpdateCommentResult>> UpdateComment(UpdateCommentCommand req, CancellationToken cancellation);
    }
}
