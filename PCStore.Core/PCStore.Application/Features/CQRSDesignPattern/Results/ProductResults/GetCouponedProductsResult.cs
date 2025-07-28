using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.ProductResults
{
    public class GetCouponedProductsResult
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; } = null!;
        public bool CouponIsPercentage { get; set; }
        public decimal CouponValue { get; set; }
        public List<GetCouponedProductDTO> CouponedProducts { get; set; } = null!;
    }
    public class GetCouponedProductDTO 
    {
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public required decimal ProductPrice { get; set; }
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
