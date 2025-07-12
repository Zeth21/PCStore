namespace PCStore.Application.Features.CQRSDesignPattern.Results.AttributeDefinitionResults
{
    public class GetProductAttributesResult
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string? Unit { get; set; }
    }
}
