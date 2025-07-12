using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductAttributeCommands;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductCommands;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductPhotoCommands;

namespace PCStore.Application.Services.ProductService.ProductServiceCommands
{
    public class ServiceCreateProductCommand
    {
        public required CreateProductCommand Information { get; set; }
        public required BulkCreateProductAttributesCommand Attributes { get; set; }
        public required CreateProductPhotoCommand Photos { get; set; }
    }
}
