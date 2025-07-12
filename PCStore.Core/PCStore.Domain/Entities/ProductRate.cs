using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCStore.Domain.Entities
{
    public class ProductRate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductRateId { get; set; }
        public decimal ProductRateScore { get; set; }
        public string? ProductRateUserId { get; set; }
        public int ProductRateProductId { get; set; }

        [ForeignKey("ProductRateUserId")]
        public User? User { get; set; }

        [ForeignKey("ProductRateProductId")]
        public Product? Product { get; set; }

    }
}
