namespace PCStore.Application.Features.CQRSDesignPattern.Results.ProductResults
{
    public class GetProductInformationsResult
    {
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal? ProductPrice { get; set; }
        public string? ProductMainPhotoPath { get; set; }
        public int? ProductTotalRate { get; set; }
        public decimal? ProductRateScore { get; set; }
        public decimal? OldPrice { get; set; }
        public bool? IsDiscountPercentage {get;set;}
        public decimal? DiscountRate { get; set; }

    }
}
