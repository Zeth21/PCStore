namespace PCStore.Application.Features.CQRSDesignPattern.Commands.OrderProductListCommands
{
    public class UpdateOrderProductListCommand
    {
        public int ListId { get; set; }
        public int ProductId { get; set; }
        public byte ProductQuantity { get; set; }
        public int ProductCost { get; set; }
        public int OrderId { get; set; }

    }
}
