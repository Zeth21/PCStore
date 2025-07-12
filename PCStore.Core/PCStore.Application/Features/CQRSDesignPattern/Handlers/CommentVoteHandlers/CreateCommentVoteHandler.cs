using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CommentVoteCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CommentVoteResults;
using PCStore.Domain.Entities;
using PCStore.Domain.Enum;
using PCStore.Persistence.Context;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.CommentVoteHandlers
{
    public class CreateCommentVoteHandler(ProjectDbContext projectDbContext, IMapper mapper) : IRequestHandler<CreateCommentVoteCommand, TaskResult<CreateCommentVoteResult>>
    {
        private readonly ProjectDbContext _projectDbContext = projectDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<TaskResult<CreateCommentVoteResult>> Handle(CreateCommentVoteCommand request, CancellationToken cancellationToken)
        {
            var comment = await _projectDbContext.Comments.FindAsync(new object[] { request.CommentVoteCommentId }, cancellationToken);
            if (comment is null)
                return TaskResult<CreateCommentVoteResult>.NotFound("comment not found!");
            var user = await _projectDbContext.Users.FindAsync(new object[] { request.CommentVoteUserId }, cancellationToken);
            if (user is null)
                return TaskResult<CreateCommentVoteResult>.NotFound("User not found!");
            var result = new CreateCommentVoteResult { };
            var vote = await _projectDbContext.CommentVotes
                .Where(x => x.CommentVoteUserId == request.CommentVoteUserId && x.CommentVoteCommentId == request.CommentVoteCommentId)
                .FirstOrDefaultAsync(cancellationToken);
            if (vote is not null)
            {
                if (vote.CommentVoteValue == request.CommentVoteValue)
                    return TaskResult<CreateCommentVoteResult>.Fail("No changes found!");
                vote.CommentVoteValue = request.CommentVoteValue;
                if (request.CommentVoteValue == VoteType.Up)
                {
                    comment.CommentUpVoteCount += 1;
                    comment.CommentDownVoteCount -= 1;
                    result.UpVoteCount = comment.CommentUpVoteCount;
                    result.DownVoteCount = comment.CommentDownVoteCount;
                }
                else
                {
                    comment.CommentUpVoteCount -= 1;
                    comment.CommentDownVoteCount += 1;
                    result.UpVoteCount = comment.CommentUpVoteCount;
                    result.DownVoteCount = comment.CommentDownVoteCount;
                }

                await _projectDbContext.SaveChangesAsync(cancellationToken);
                return TaskResult<CreateCommentVoteResult>.Success("Vote has added successfully!", data: result);
            }
            var newVote = _mapper.Map<CommentVote>(request);
            if (request.CommentVoteValue == VoteType.Up)
            {
                comment.CommentUpVoteCount += 1;
                result.UpVoteCount = comment.CommentUpVoteCount;
                result.DownVoteCount = comment.CommentDownVoteCount;
            }
            else
            {
                comment.CommentDownVoteCount += 1;
                result.UpVoteCount = comment.CommentUpVoteCount;
                result.DownVoteCount = comment.CommentDownVoteCount;
            }
            await _projectDbContext.AddAsync(newVote, cancellationToken);
            await _projectDbContext.SaveChangesAsync(cancellationToken);

            return TaskResult<CreateCommentVoteResult>.Success("Vote has added successfully!", data: result);
        }
    }
}
