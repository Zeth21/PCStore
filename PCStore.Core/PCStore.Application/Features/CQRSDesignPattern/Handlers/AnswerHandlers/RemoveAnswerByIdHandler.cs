using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.AnswerCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Persistence.Context;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.AnswerHandlers
{
    public class RemoveAnswerByIdHandler(ProjectDbContext projectDbContext) : IRequestHandler<RemoveAnswerByIdCommand, Result>
    {
        private readonly ProjectDbContext _projectDbContext = projectDbContext;
        public async Task<Result> Handle(RemoveAnswerByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var answer = await _projectDbContext.Answers.Where(x => x.AnswerId == request.AnswerId).FirstOrDefaultAsync(cancellationToken);
                if (answer is null)
                    return Result.NotFound(message: "Answer not found!");
                _projectDbContext.Answers.Remove(answer);
                await _projectDbContext.SaveChangesAsync(cancellationToken);
                return Result.Success(message: "Answer is removed successfully!");
            }
            catch (Exception ex)
            {
                return Result.Fail(message: ex.Message);
            }
        }
    }
}
