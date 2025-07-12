using AutoMapper;
using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.CouponHandlers
{
    public class DeactiveCouponHandler(ProjectDbContext context) : IRequestHandler<DeactiveCouponCommand, Result>
    {
        public async Task<Result> Handle(DeactiveCouponCommand request, CancellationToken cancellationToken)
        {
            var checkCoupon = await context.Coupons.FindAsync(request.CouponId,cancellationToken);
            if (checkCoupon is null || !checkCoupon.CouponIsActive)
                return Result.NotFound("Coupon not found!");
            checkCoupon.CouponIsActive = false;
            checkCoupon.CouponEndTime = DateTime.Now;
            try 
            {
                await context.SaveChangesAsync(cancellationToken);
            }
            catch(Exception ex) 
            {
                return Result.Fail("Something went wrong! " + ex.Message);
            }
            return Result.Success("Coupon activeness updated successfully!");
        }
    }
}
