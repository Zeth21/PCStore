using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCStore.Domain.Entities
{
    public class ProductAttribute
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [ForeignKey("AttributeDefinition")]
        public int AttributeDefinitionId { get; set; }

        public string Value { get; set; } = string.Empty;

        public Product? Product { get; set; }
        public AttributeDefinition? AttributeDefinition { get; set; }
    }
}
