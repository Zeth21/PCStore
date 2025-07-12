namespace PCStore.Application.Features.CQRSDesignPattern.Commands.CouponCommands
{
    public class RemoveCouponCommand
    {
        public int CouponId { get; set; }

        public RemoveCouponCommand(int couponId)
        {
            CouponId = couponId;
        }
    }
}
