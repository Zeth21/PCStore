using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Commands.AnswerVoteCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.AnswerVoteResults;

namespace PCStore.Application.Services.AnswerVoteService
{
    public class AnswerVoteService(IMediator mediator) : IAnswerVoteService
    {
        private readonly IMediator _mediator = mediator;
        public async Task<TaskResult<CreateAnswerVoteResult>> CreateAnswerVote(CreateAnswerVoteCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result;
        }

        public async Task<Result> RemoveAnswerVote(RemoveAnswerVoteCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result;
        }
    }
}
