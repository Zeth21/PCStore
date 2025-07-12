using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using System.Text.Json.Serialization;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.ProductPhotoCommands
{
    public class RemoveProductPhotoCommand : IRequest<Result>
    {
        [JsonIgnore]
        public int ProductId { get; set; }

        public required List<int> PhotoIds  { get; set; }

        [JsonIgnore]
        public string? WebRootPath { get; set; } = string.Empty;
    }
}
