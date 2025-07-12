namespace PCStore.Application.Features.CQRSDesignPattern.Commands.OrderStatusCommands
{
    public class CreateOrderStatusCommand
    {
        public DateTime StatusDate { get; set; }
        public int StatusNameId { get; set; }
        public int OrderId { get; set; }
    }
}
