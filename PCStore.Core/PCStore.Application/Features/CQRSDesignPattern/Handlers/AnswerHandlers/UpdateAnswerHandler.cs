using AutoMapper;
using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Commands.AnswerCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.AnswerResults;
using PCStore.Persistence.Context;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.AnswerHandlers
{
    public class UpdateAnswerHandler(ProjectDbContext context, IMapper mapper) : IRequestHandler<UpdateAnswerCommand, TaskResult<UpdateAnswerResult>>
    {
        private readonly ProjectDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        async Task<TaskResult<UpdateAnswerResult>> IRequestHandler<UpdateAnswerCommand, TaskResult<UpdateAnswerResult>>.Handle(UpdateAnswerCommand request, CancellationToken cancellationToken)
        {
            var answer = await _context.Answers.FindAsync(request.AnswerId);
            if (answer is null)
                return TaskResult<UpdateAnswerResult>.NotFound("Answer not found!");
            if (answer.AnswerUserId != request.AnswerUserId)
                return TaskResult<UpdateAnswerResult>.NotFound("User is not valid!");
            if (answer.AnswerText == request.AnswerText)
                return TaskResult<UpdateAnswerResult>.Fail("No changes found!");
            answer.AnswerText = request.AnswerText;
            await _context.SaveChangesAsync(cancellationToken);
            var result = _mapper.Map<UpdateAnswerResult>(answer);
            return TaskResult<UpdateAnswerResult>.Success(message: "Answer is updated successfully!", data: result);
        }
    }
}
