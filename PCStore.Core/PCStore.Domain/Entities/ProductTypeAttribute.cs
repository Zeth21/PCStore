using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCStore.Domain.Entities
{
    public class ProductTypeAttribute
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("ProductType")]
        public int ProductTypeId { get; set; }

        [ForeignKey("AttributeDefinition")]
        public int AttributeDefinitionId { get; set; }

        public ProductType? ProductType { get; set; }
        public AttributeDefinition? AttributeDefinition { get; set; }
        public ProductTypeAttribute() { }
        public ProductTypeAttribute(int productTypeId, int attributeDefinitionId)
        {
            ProductTypeId = productTypeId;
            AttributeDefinitionId = attributeDefinitionId;
        }
    }
}
