using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCStore.Domain.Entities
{
    public class AttributeDefinition
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string DataType { get; set; }
        public bool IsRequired { get; set; }
        public string? Unit { get; set; }
        public ICollection<ProductTypeAttribute> ProductTypeAttributes { get; set; } = [];
        public ICollection<ProductAttribute> ProductAttributes { get; set; } = [];
    }
}
