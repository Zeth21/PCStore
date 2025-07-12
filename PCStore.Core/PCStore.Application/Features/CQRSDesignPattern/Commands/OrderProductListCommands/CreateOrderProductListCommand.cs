namespace PCStore.Application.Features.CQRSDesignPattern.Commands.OrderProductListCommands
{
    public class CreateOrderProductListCommand
    {
        public int ProductId { get; set; }
        public byte ProductQuantity { get; set; }
        public int ProductCost { get; set; }
        public int OrderId { get; set; }

    }
}
