namespace PCStore.Application.Features.CQRSDesignPattern.Commands.OrderStatusCommands
{
    public class UpdateOrderStatusCommand
    {
        public int StatusId { get; set; }
        public DateTime StatusDate { get; set; }
        public int StatusNameId { get; set; }
        public int OrderId { get; set; }
    }
}
