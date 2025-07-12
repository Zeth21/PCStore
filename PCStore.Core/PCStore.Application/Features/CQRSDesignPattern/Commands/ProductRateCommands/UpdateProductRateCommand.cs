namespace PCStore.Application.Features.CQRSDesignPattern.Commands.ProductRateCommands
{
    public class UpdateProductRateCommand
    {
        public int ProductRateId { get; set; }
        public decimal ProductRateScore { get; set; }
    }
}
