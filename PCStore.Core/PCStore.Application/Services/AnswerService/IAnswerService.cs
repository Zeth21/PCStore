using PCStore.Application.Features.CQRSDesignPattern.Commands.AnswerCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.AnswerQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.AnswerResults;

namespace PCStore.Application.Services.AnswerService
{
    public interface IAnswerService
    {
        Task<TaskListResult<GetAnswersByCommentIdResult>> GetAnswers(GetAnswersByCommentIdQuery request, CancellationToken cancellationToken);
        Task<Result> RemoveAnswer(RemoveAnswerByIdCommand request, CancellationToken cancellationToken);
        Task<TaskResult<CreateAnswerResult>> CreateAnswer(CreateAnswerCommand request, CancellationToken cancellationToken);
        Task<TaskResult<UpdateAnswerResult>> UpdateAnswer(UpdateAnswerCommand request, CancellationToken cancellationToken);
    }
}
