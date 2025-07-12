using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductAttributeResults;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.ProductAttributeCommands
{
    public class BulkCreateProductAttributesCommand : IRequest<TaskListResult<CreateProductAttributesResult>>
    {
        public int ProductId { get; set; }
        public required List<CreateProductAttributesCommand> Attributes { get; set; }
    }
}
