using PCStore.UI.Models.Results.CartResults;

namespace PCStore.UI.Models.Results.OrderResults
{
    public class ServiceCreateOrderResult
    {
        public int OrderId { get; set; }
        public decimal OrderTotalCost { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderAddressId { get; set; }
        public DateTime StatusDate { get; set; }
        public required string StatusName { get; set; }
        public decimal? TotalCouponDiscount { get; set; }
        public decimal? TotalDiscount { get; set; }
        public decimal? OldTotalCost { get; set; }
        public required List<GetShopCartItemsResult> CartItems { get; set; }
    }

}
