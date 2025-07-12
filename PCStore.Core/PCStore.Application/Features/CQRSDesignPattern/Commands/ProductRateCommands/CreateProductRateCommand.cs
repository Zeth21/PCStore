namespace PCStore.Application.Features.CQRSDesignPattern.Commands.ProductRateCommands
{
    public class CreateProductRateCommand
    {
        public decimal ProductRateScore { get; set; }
        public required string ProductRateUserId { get; set; }
        public int ProductRateProductId { get; set; }
    }
}
