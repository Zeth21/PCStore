using PCStore.Application.Features.CQRSDesignPattern.Results.ProductAttributeResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductPhotoResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductResults;

namespace PCStore.Application.Services.ProductService.ProductServiceResults
{
    public class ServiceCreateProductResult
    {
        public CreateProductResult? Information { get; set; }
        public List<CreateProductAttributesResult>? Attributes { get; set; }
        public List<GetPhotosByProductIdResult>? Photos { get; set; }
    }
}
