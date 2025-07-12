using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CommentCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.CommentQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CommentResults;

namespace PCStore.Application.Services.CommentService
{
    public class CommentService(IMediator mediator) : ICommentService
    {
        private readonly IMediator _mediator = mediator;

        public async Task<TaskResult<CreateCommentResult>> CreateComment(CreateCommentCommand request, CancellationToken cancellation)
        {
            var result = await _mediator.Send(request,cancellation);
            return result;
        }

        public async Task<TaskListResult<GetCommentsByProductIdResult>> GetComments(GetCommentsByProductIdQuery request, CancellationToken cancellationToken)
        {
            var comments = await _mediator.Send(request, cancellationToken);
            return comments;
        }

        public async Task<TaskListResult<GetQuestionsByProductIdResult>> GetQuestions(GetQuestionsByProductIdQuery request, CancellationToken cancellationToken)
        {
            var questions = await _mediator.Send(request, cancellationToken);
            return questions;
        }

        public async Task<Result> RemoveComment(RemoveCommentCommand request, CancellationToken cancellation)
        {
            var result = await _mediator.Send(request,cancellation);
            return result;
        }

        public async Task<TaskResult<UpdateCommentResult>> UpdateComment(UpdateCommentCommand req, CancellationToken cancellation)
        {
            var result = await _mediator.Send(req,cancellation);
            return result;
        }
    }
}
