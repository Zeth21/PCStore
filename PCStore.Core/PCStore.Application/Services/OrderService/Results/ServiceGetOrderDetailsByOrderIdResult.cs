using PCStore.Application.Features.CQRSDesignPattern.Results.OrderProductListResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Services.OrderService.Results
{
    public class ServiceGetOrderDetailsByOrderIdResult
    {
        public int OrderId { get; set; }
        public decimal OrderTotalCost { get; set; }
        public decimal? DiscountTotal => OrderProducts?.Sum(x => x.OldTotalPrice.HasValue ? (x.OldTotalPrice - x.TotalPrice) : 0);
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
}
