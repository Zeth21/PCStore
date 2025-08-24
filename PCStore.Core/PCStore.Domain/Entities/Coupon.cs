using PCStore.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCStore.Domain.Entities
{
    public class Coupon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CouponId { get; set; }
        public decimal CouponValue { get; set; }
        public bool CouponIsPercentage { get; set; } = false;
        
        [Range(1, int.MaxValue)]
        public int CouponMaxUsage { get; set; }

        [Range(1, int.MaxValue)]
        public int CouponMaxUsagePerUser { get; set; } = 1;

        [Range(0, int.MaxValue)]
        public int CouponMinOrderAmount { get; set; } = 0;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime CouponStartTime { get; set; } = DateTime.Now;
        public DateTime? CouponEndTime { get; set; }
        public bool CouponIsActive { get; set; } = true;
        public required string Description { get; set; }
        public required string CouponCode { get; set; }
        public CouponTargetType CouponTargetType { get; set; }

        public ICollection<CouponProduct>? CouponProducts { get; set; }
        public ICollection<CouponUsage>? CouponUsages { get; set; }
        public ICollection<CouponProductType>? CouponProductTypes { get; set; }
        public ICollection<CouponCategory>? CouponCategories { get; set; }
        public ICollection<CouponBrand>? CouponBrands { get; set; }

    }
}
