using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductTypeResults;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.ProductTypeCommands
{
    public class UpdateTypeCommand : IRequest<TaskResult<UpdateTypeResult>>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
