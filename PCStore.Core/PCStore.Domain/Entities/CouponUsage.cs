using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCStore.Domain.Entities
{
    public class CouponUsage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? CouponUsageUserId { get; set; }
        public int CouponUsageCouponId { get; set; }
        public int CouponUsageOrderId { get; set; }
        public decimal DiscountTotal { get; set; }


        [ForeignKey("CouponUsageUserId")]
        public User? User { get; set; }

        [ForeignKey("CouponUsageCouponId")]
        public Coupon? Coupon { get; set; }

        [ForeignKey("CouponUsageOrderId")]
        public Order? Order { get; set; }
    }
}
