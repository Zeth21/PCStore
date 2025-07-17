using PCStore.Application.Features.CQRSDesignPattern.Results.OrderResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.OrderStatusResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.ShoppingCartItemResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Services.OrderService.Results
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
