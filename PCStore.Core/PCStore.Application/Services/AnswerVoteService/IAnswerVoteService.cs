using PCStore.Application.Features.CQRSDesignPattern.Commands.AnswerVoteCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.AnswerVoteResults;

namespace PCStore.Application.Services.AnswerVoteService
{
    public interface IAnswerVoteService
    {
        public Task<TaskResult<CreateAnswerVoteResult>> CreateAnswerVote(CreateAnswerVoteCommand request, CancellationToken cancellationToken);
        public Task<Result> RemoveAnswerVote(RemoveAnswerVoteCommand request, CancellationToken cancellationToken);
    }
}
