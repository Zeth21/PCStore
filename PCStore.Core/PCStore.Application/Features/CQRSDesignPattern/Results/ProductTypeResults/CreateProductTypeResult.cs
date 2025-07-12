using PCStore.Application.Features.CQRSDesignPattern.Results.AttributeDefinitionResults;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.ProductTypeResults
{
    public class CreateProductTypeResult
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<GetTypeAttributesByIdResult> Attributes { get; set; } = [];
    }
}
