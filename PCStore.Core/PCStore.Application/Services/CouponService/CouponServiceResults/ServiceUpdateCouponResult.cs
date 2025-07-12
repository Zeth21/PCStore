using PCStore.Application.Features.CQRSDesignPattern.Results.CouponBrandResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponCategoryResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponProductsHandler;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponProductTypeResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponResults;


namespace PCStore.Application.Services.CouponService.CouponServiceResults
{
    public class ServiceUpdateCouponResult
    {
        public required UpdateCouponResult CouponInformation { get; set; }
        public CreateCouponProductTypeResult? TypeInformation { get; set; }
        public List<ListCreateCouponProductResult>? ProductListInformation { get; set; }
        public CreateCouponBrandResult? BrandInformation { get; set; }
        public CreateCouponCategoryResult? CategoryInformation { get; set; }
    }
}
