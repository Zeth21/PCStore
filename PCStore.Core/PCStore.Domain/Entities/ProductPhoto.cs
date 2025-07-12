using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCStore.Domain.Entities
{
    public class ProductPhoto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhotoId { get; set; }
        public int PhotoProductId { get; set; }
        public required string PhotoPath { get; set; }
        public required string PhotoName { get; set; }

        [ForeignKey("PhotoProductId")]
        public Product? Product { get; set; }
    }
}
