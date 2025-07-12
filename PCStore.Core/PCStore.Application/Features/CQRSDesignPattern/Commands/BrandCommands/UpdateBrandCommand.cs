namespace PCStore.Application.Features.CQRSDesignPattern.Commands.BrandCommands
{
    public class UpdateBrandCommand
    {
        public int BrandId { get; set; }
        public required string BrandName { get; set; }
    }
}
