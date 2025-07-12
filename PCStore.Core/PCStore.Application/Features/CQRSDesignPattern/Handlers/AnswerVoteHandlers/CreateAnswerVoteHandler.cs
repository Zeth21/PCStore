using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.AnswerVoteCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.AnswerVoteResults;
using PCStore.Domain.Entities;
using PCStore.Domain.Enum;
using PCStore.Persistence.Context;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.AnswerVoteHandlers
{
    public class CreateAnswerVoteHandler(ProjectDbContext projectDbContext, IMapper mapper) : IRequestHandler<CreateAnswerVoteCommand, TaskResult<CreateAnswerVoteResult>>
    {
        private readonly ProjectDbContext _projectDbContext = projectDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<TaskResult<CreateAnswerVoteResult>> Handle(CreateAnswerVoteCommand request, CancellationToken cancellationToken)
        {
            var answer = await _projectDbContext.Answers.FindAsync(new object[] { request.AnswerVoteAnswerId }, cancellationToken);
            if (answer is null)
                return TaskResult<CreateAnswerVoteResult>.NotFound("Answer not found!");
            var user = await _projectDbContext.Users.FindAsync(new object[] { request.AnswerVoteUserId }, cancellationToken);
            if (user is null)
                return TaskResult<CreateAnswerVoteResult>.NotFound("User not found!");
            var result = new CreateAnswerVoteResult { };
            var vote = await _projectDbContext.AnswerVotes
                .Where(x => x.AnswerVoteUserId == request.AnswerVoteUserId && x.AnswerVoteAnswerId == request.AnswerVoteAnswerId)
                .FirstOrDefaultAsync(cancellationToken);
            if (vote is not null)
            {
                if (vote.AnswerVoteValue == request.AnswerVoteValue)
                    return TaskResult<CreateAnswerVoteResult>.Fail("No changes found!");
                vote.AnswerVoteValue = request.AnswerVoteValue;
                if (request.AnswerVoteValue == VoteType.Up)
                {
                    answer.AnswerUpVoteCount += 1;
                    answer.AnswerDownVoteCount -= 1;
                    result.UpVoteCount = answer.AnswerUpVoteCount;
                    result.DownVoteCount = answer.AnswerDownVoteCount;
                }
                else
                {
                    answer.AnswerUpVoteCount -= 1;
                    answer.AnswerDownVoteCount += 1;
                    result.UpVoteCount = answer.AnswerUpVoteCount;
                    result.DownVoteCount = answer.AnswerDownVoteCount;
                }

                await _projectDbContext.SaveChangesAsync(cancellationToken);
                return TaskResult<CreateAnswerVoteResult>.Success("Vote has added successfully!", data: result);
            }
            var newVote = _mapper.Map<AnswerVote>(request);
            if (request.AnswerVoteValue == VoteType.Up)
            {
                answer.AnswerUpVoteCount += 1;
                result.UpVoteCount = answer.AnswerUpVoteCount;
                result.DownVoteCount = answer.AnswerDownVoteCount;
            }
            else
            {
                answer.AnswerDownVoteCount += 1;
                result.UpVoteCount = answer.AnswerUpVoteCount;
                result.DownVoteCount = answer.AnswerDownVoteCount;
            }
            await _projectDbContext.AddAsync(newVote, cancellationToken);
            await _projectDbContext.SaveChangesAsync(cancellationToken);

            return TaskResult<CreateAnswerVoteResult>.Success("Vote has added successfully!", data: result);
        }
    }
}
