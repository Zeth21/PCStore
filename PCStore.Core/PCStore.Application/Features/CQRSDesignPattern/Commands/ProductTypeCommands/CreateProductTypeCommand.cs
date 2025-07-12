using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductTypeResults;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.ProductTypeCommands
{
    public class CreateProductTypeCommand : IRequest<TaskResult<CreateProductTypeResult>>
    {
        public string? Name { get; set; }
        public List<int>? AttributeIds { get; set; }
    }
}
