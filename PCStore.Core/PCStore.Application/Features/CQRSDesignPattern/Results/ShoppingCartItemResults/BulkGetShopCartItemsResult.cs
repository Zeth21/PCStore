using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.ShoppingCartItemResults
{
    public class BulkGetShopCartItemsResult
    {
        public List<GetShopCartItemsResult> CartItems { get; set; } = [];
        public decimal TotalCost => TotalCouponDiscount.HasValue ? CartItems.Sum(x => x.TotalPrice) - (TotalCouponDiscount ?? 0m) : CartItems.Sum(x => x.TotalPrice);
        public decimal? TotalCouponDiscount { get; set; }
        public decimal? TotalDiscount { get; set; }
        public decimal? OldTotalCost => CartItems.Sum(x => x.OldPrice * x.ItemCount);
    }
}
