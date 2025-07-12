namespace PCStore.Application.Features.CQRSDesignPattern.Commands.CouponUsageCommand
{
    public class CreateCouponUsageCommand
    {
        public DateTime CouponUsageTime { get; set; } = DateTime.Now;
        public required string CouponUsageUserId { get; set; }
        public int CouponUsageCouponId { get; set; }
        public int CouponUsageOrderId { get; set; }

    }
}
