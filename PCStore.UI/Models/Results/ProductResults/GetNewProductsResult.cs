namespace PCStore.UI.Models.Results.ProductResults
{
    public class GetNewProductsResult
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string? ProductMainPhotoPath { get; set; }
        public short ProductStock { get; set; }
        public string? ProductBrandName { get; set; }
        public string? ProductCategoryName { get; set; }
        public bool ProductIsAvailable { get; set; }
        public int ProductTotalRate { get; set; }
        public decimal ProductRateScore { get; set; }
        public decimal? DiscountRate { get; set; }
        public decimal? OldPrice { get; set; }
        public bool? IsDiscountPercentage { get; set; }
        public string? ProductTypeName { get; set; }
    }
}
