namespace PCStore.UI.Models.Results.OrderResults
{
    public class ServiceGetOrderDetailsByOrderIdResult
    {
        public int OrderId { get; set; }
        public decimal OrderTotalCost { get; set; }
        public decimal? DiscountTotal { get; set; }
        public DateTime OrderDate { get; set; }
        public bool OrderIsActive { get; set; }
        public string OrderAddress { get; set; } = null!;
        public int StatusId { get; set; }
        public DateTime StatusDate { get; set; }
        public string StatusName { get; set; } = null!;
        public decimal? CouponDiscountTotal { get; set; }
        public string? CouponCode { get; set; }
        public int? CouponId { get; set; }
        public List<GetOrderProductListsByOrderIdResult> OrderProducts { get; set; } = null!;
    }
    public class GetOrderProductListsByOrderIdResult
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
    }
}
