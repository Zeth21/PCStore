namespace PCStore.Application.Features.CQRSDesignPattern.Results.ProductAttributeResults
{
    public class CreateProductAttributesResult
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Value { get; set; }
        public string? Unit { get; set; }
    }
}
