using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponBrandCommands;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponCategoryCommands;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponCommands;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponProductCommands;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponProductTypeCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Services.CouponService.CouponServiceCommands
{
    public class ServiceUpdateCouponCommand 
    {
        public required UpdateCouponCommand CouponInformation { get; set; }
        public CreateCouponProductTypeCommand? TypeInformation { get; set; }
        public ListCreateCouponProductCommand? ProductListInformation { get; set; }
        public CreateCouponBrandCommand? BrandInformation { get; set; }
        public CreateCouponCategoryCommand? CategoryInformation { get; set; }
    }
}
