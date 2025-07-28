using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponCommands;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponProductCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.CouponQueries;
using PCStore.Application.Features.CQRSDesignPattern.Queries.CouponUsageQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponProductsHandler;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponUsageResults;
using PCStore.Application.Services.CouponService.CouponServiceCommands;
using PCStore.Application.Services.CouponService.CouponServiceResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Services.CouponService
{
    public interface ICouponService
    {
        Task<TaskResult<AdminGetCouponByIdResult>> AdminGetCouponById(AdminGetCouponByIdQuery request, CancellationToken cancellation);
        Task<TaskResult<CreateCouponResult>> CreateCoupon(CreateCouponCommand request, CancellationToken cancellation);
        Task<Result> DeactiveCoupon(DeactiveCouponCommand request, CancellationToken cancellation);
        Task<Result> ActiveCoupon(ActiveCouponCommand request, CancellationToken cancellation);
        Task<TaskListResult<AdminGetAllCouponsResult>> AdminGetAllCoupons(AdminGetAllCouponsQuery req, CancellationToken cancellation);
        Task<TaskResult<ServiceUpdateCouponResult>> UpdateCoupon(ServiceUpdateCouponCommand req, CancellationToken cancellation);
        Task<Result> CouponIsValid(IsCouponValidQuery req, CancellationToken cancellation);
        Task<TaskListResult<GetAllCouponUsagesResult>> GetAllCouponUsages(GetAllCouponUsagesQuery request, CancellationToken cancellation);
    }
}
