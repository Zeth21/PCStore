using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductResults;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.ProductCommands
{
    public class UpdateProductCommand : IRequest<TaskResult<UpdateProductResult>>
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal? ProductPrice { get; set; }
        public string? ProductMainPhotoPath { get; set; }
        public short? ProductStock { get; set; }
        public int? ProductBrandId { get; set; }
        public int? ProductCategoryId { get; set; }
    }
}
