using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.AnswerResults;
using System.Text.Json.Serialization;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.AnswerCommands
{
    public class CreateAnswerCommand : IRequest<TaskResult<CreateAnswerResult>>
    {
        [JsonIgnore]
        public string? AnswerUserId { get; set; } = "";
        public required string AnswerText { get; set; }
        public int AnswerCommentId { get; set; }
    }
}
