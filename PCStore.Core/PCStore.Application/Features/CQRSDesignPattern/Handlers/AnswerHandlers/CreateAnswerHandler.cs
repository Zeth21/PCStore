using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.AnswerCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.AnswerResults;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.AnswerHandlers
{
    public class CreateAnswerHandler(ProjectDbContext projectDbContext, IMapper mapper) : IRequestHandler<CreateAnswerCommand, TaskResult<CreateAnswerResult>>
    {
        private readonly ProjectDbContext _projectDbContext = projectDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<TaskResult<CreateAnswerResult>> Handle(CreateAnswerCommand request, CancellationToken cancellationToken)
        {
            var checkComment = await _projectDbContext.Comments.Where(x => x.CommentId == request.AnswerCommentId).FirstOrDefaultAsync(cancellationToken);
            var checkUser = await _projectDbContext.Users.Where(x => x.Id == request.AnswerUserId).FirstOrDefaultAsync(cancellationToken);
            if (checkUser is null)
                return TaskResult<CreateAnswerResult>.NotFound(message: "No user found!");
            else if (checkComment is null)
                return TaskResult<CreateAnswerResult>.NotFound(message: "No comment found!");
            var answer = _mapper.Map<Answer>(request);
            await _projectDbContext.Answers.AddAsync(answer);
            var task = await _projectDbContext.SaveChangesAsync(cancellationToken);
            if (task == 0)
            {
                return TaskResult<CreateAnswerResult>.Fail(message: "Process failed!");
            }
            var result = _mapper.Map<CreateAnswerResult>(answer);
            return TaskResult<CreateAnswerResult>.Success(message: "Answer is created successfully!", data: result);
        }
    }
}
