namespace PCStore.Application.Features.CQRSDesignPattern.Commands.CouponUsageCommand
{
    public class RemoveCouponUsageCommand
    {
        public int CouponUsageId { get; set; }

        public RemoveCouponUsageCommand(int couponUsageId)
        {
            CouponUsageId = couponUsageId;
        }
    }
}
