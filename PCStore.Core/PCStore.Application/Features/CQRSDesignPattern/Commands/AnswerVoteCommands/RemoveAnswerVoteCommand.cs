using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using System.Text.Json.Serialization;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.AnswerVoteCommands
{
    public class RemoveAnswerVoteCommand : IRequest<Result>
    {
        public int AnswerVoteId { get; set; }
        [JsonIgnore]
        public string? UserId { get; set; } = "";
    }
}
