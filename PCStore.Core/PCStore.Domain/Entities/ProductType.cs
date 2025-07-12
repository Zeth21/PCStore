using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCStore.Domain.Entities
{
    public class ProductType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<ProductTypeAttribute> ProductTypeAttributes { get; set; } = [];
        public ICollection<Product> Products { get; set; } = [];
    }


}
