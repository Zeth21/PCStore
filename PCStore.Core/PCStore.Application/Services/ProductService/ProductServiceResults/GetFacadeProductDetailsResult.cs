using PCStore.Application.Features.CQRSDesignPattern.Results.AttributeDefinitionResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.CommentResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductPhotoResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductResults;

namespace PCStore.Application.Services.ProductService.ProductServiceResults
{
    public class GetFacadeProductDetailsResult
    {
        public string? BrandName { get; set; }
        public GetProductInformationsResult? Informations { get; set; }
        public List<GetProductAttributesResult>? Attributes { get; set; }
        public List<GetQuestionsByProductIdResult>? Questions { get; set; }
        public List<GetCommentsByProductIdResult>? Comments { get; set; }
        public List<GetPhotosByProductIdResult>? Photos { get; set; }
    }
}
