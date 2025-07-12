namespace PCStore.Application.Features.CQRSDesignPattern.Commands.ProductRateCommands
{
    public class RemoveProductRateCommand
    {
        public int ProductRateId { get; set; }

        public RemoveProductRateCommand(int productRateId)
        {
            ProductRateId = productRateId;
        }
    }
}
