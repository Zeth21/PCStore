namespace PCStore.UI.Models.Commands.OrderCommands
{
    public class ServiceCreateOrderCommand
    {
        public int? CouponId { get; set; } = null;
        public int AddressId { get; set; }
    }
}
