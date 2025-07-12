using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductAttributeCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductResults;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.ProductCommands
{
    public class UpdateProductTypeCommand : IRequest<TaskResult<UpdateProductTypeResult>>
    {
        public int ProductId { get; set; }
        public int TypeId { get; set; }
        public required List<CreateProductAttributesCommand> NewAttributes { get; set; }
    }
}
