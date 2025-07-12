using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductResults;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.ProductCommands
{
    public class CreateProductCommand : IRequest<TaskResult<CreateProductResult>>
    {
        public required string ProductName { get; set; }
        public required decimal ProductPrice { get; set; }
        public string ProductMainPhotoPath { get; set; } = "def_product_mainphoto.jpg";
        public short ProductStock { get; set; } = 1;
        public int ProductBrandId { get; set; }
        public int ProductCategoryId { get; set; }
        public int ProductTypeId { get; set; }
    }
}
