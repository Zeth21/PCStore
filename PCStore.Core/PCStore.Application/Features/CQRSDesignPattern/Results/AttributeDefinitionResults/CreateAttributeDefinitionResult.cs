namespace PCStore.Application.Features.CQRSDesignPattern.Results.AttributeDefinitionResults
{
    public class CreateAttributeDefinitionResult
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? DataType { get; set; }
        public bool IsRequired { get; set; }
        public string? Unit { get; set; }
    }
}
