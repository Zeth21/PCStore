using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.AnswerVoteCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Domain.Enum;
using PCStore.Persistence.Context;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.AnswerVoteHandlers
{
    public class RemoveAnswerVoteHandler(ProjectDbContext projectDbContext) : IRequestHandler<RemoveAnswerVoteCommand, Result>
    {
        private readonly ProjectDbContext _projectDbContext = projectDbContext;

        public async Task<Result> Handle(RemoveAnswerVoteCommand request, CancellationToken cancellationToken)
        {
            var vote = await _projectDbContext.AnswerVotes
                .Where(x => x.AnswerVoteUserId == request.UserId && x.AnswerVoteId == request.AnswerVoteId)
                .FirstOrDefaultAsync(cancellationToken);
            if (vote is null)
                return Result.NotFound(message: "Vote not found!");
            var answer = await _projectDbContext.Answers.FindAsync(vote.AnswerVoteAnswerId);
            if (answer is null)
                return Result.NotFound(message: "Answer not found!");
            if (vote.AnswerVoteValue == VoteType.Up)
                answer.AnswerUpVoteCount -= 1;
            else
                answer.AnswerDownVoteCount -= 1;
            _projectDbContext.AnswerVotes.Remove(vote);
            await _projectDbContext.SaveChangesAsync(cancellationToken);
            return Result.Success(message: "Vote removed successfully!");
        }
    }
}
