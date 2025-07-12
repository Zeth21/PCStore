using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Commands.AnswerCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.AnswerQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.AnswerResults;

namespace PCStore.Application.Services.AnswerService
{
    public class AnswerService(IMediator mediator) : IAnswerService
    {
        private readonly IMediator _mediator = mediator;

        public async Task<TaskResult<CreateAnswerResult>> CreateAnswer(CreateAnswerCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result;
        }

        public async Task<Result> RemoveAnswer(RemoveAnswerByIdCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result;
        }

        public Task<TaskResult<UpdateAnswerResult>> UpdateAnswer(UpdateAnswerCommand request, CancellationToken cancellationToken)
        {
            var result = _mediator.Send(request, cancellationToken);
            return result;
        }

        async Task<TaskListResult<GetAnswersByCommentIdResult>> IAnswerService.GetAnswers(GetAnswersByCommentIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result;
        }

    }
}
