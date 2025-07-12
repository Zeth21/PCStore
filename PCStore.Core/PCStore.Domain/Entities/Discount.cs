using PCStore.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCStore.Domain.Entities
{
    public class Discount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DiscountId { get; set; }
        [MaxLength(75)]
        public required string DiscountName { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime DiscountStartDate { get; set; } = DateTime.Now;
        public DateTime? DiscountEndDate { get; set; }
        public bool DiscountIsActive { get; set; } = true;
        public bool DiscountIsPercentage { get; set; } = true;
        public decimal DiscountRate { get; set; }
        [MaxLength(200)]
        public required string Description { get; set; }
        public ICollection<DiscountProduct>? DiscountProducts { get; set; }
        public ICollection<DiscountUsage>? DiscountUsages { get; set; }
    }
}
