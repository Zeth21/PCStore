namespace PCStore.Application.Features.CQRSDesignPattern.Commands.ProductAttributeCommands
{
    public class CreateProductAttributesCommand
    {
        public int AttributeDefinitionId { get; set; }
        public required string Value { get; set; }
    }
}
