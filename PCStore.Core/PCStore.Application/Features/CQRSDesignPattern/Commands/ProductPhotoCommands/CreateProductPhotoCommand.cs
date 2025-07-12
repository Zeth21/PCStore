using MediatR;
using Microsoft.AspNetCore.Http;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductPhotoResults;
using System.Text.Json.Serialization;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.ProductPhotoCommands
{
    public class CreateProductPhotoCommand : IRequest<TaskListResult<GetPhotosByProductIdResult>>
    {
        public int PhotoProductId { get; set; }
        public List<IFormFile> Photos { get; set; } = default!;

        [JsonIgnore]
        public string? WebRootPath { get; set; }
    }
}
