namespace PCStore.Application.Features.CQRSDesignPattern.Commands.OrderCommands
{
    public class CreateOrderCommand
    {
        public decimal OrderTotalCost { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? OrderDeliverDate { get; set; }
        public bool OrderIsActive { get; set; }
        public string? OrderAddress { get; set; }
        public string? OrderUserId { get; set; }
    }
}
