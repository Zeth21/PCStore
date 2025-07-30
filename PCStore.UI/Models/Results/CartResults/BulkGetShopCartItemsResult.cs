namespace PCStore.UI.Models.Results.CartResults
{
    public class BulkGetShopCartItemsResult
    {
        public List<GetShopCartItemsResult> CartItems { get; set; } = [];
        public decimal TotalCost { get; set; }
        public decimal? TotalCouponDiscount { get; set; }
        public decimal? TotalDiscount { get; set; }
        public decimal? OldTotalCost { get; set; }
    }
    public class GetShopCartItemsResult
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public required string BrandName { get; set; }
        public required string CategoryName { get; set; }
        public int ItemCount { get; set; }
        public decimal ProductPrice { get; set; }
        public required string ProductMainPhotoPath { get; set; }
        public required decimal ProductRateScore { get; set; }
        public int ProductTotalRate { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal? OldTotalPrice { get; set; }
        public decimal? OldPrice { get; set; }
        public bool? IsDiscountPercentage { get; set; }
        public decimal? DiscountRate { get; set; }
    }
}
