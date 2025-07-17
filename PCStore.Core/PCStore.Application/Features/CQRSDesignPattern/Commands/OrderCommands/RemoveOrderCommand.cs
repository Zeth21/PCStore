namespace PCStore.Application.Features.CQRSDesignPattern.Commands.OrderCommands
{
    public class RemoveOrderCommand
    {
        public int OrderId { get; set; }
        public RemoveOrderCommand(int orderId)
        {
            OrderId = orderId;
        }
    }
}
