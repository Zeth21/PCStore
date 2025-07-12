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
    public class ActiveCouponHandlers(ProjectDbContext context) : IRequestHandler<ActiveCouponCommand, Result>
    {
        public async Task<Result> Handle(ActiveCouponCommand request, CancellationToken cancellationToken)
        {
            var checkCoupon = await context.Coupons
                .FindAsync(request.Id, cancellationToken);
            if (checkCoupon is null || checkCoupon.CouponIsActive)
                return Result.NotFound("Coupon not found!");
            checkCoupon.CouponIsActive = true;
            checkCoupon.CouponStartTime = DateTime.Now;
            checkCoupon.CouponEndTime = null;
            try 
            {
                await context.SaveChangesAsync(cancellationToken);
            }
            catch(Exception ex) 
            {
                return Result.Fail("Something went wrong! " + ex.Message);
            }
            return Result.Success("Coupon activated successfully!");
        }
    }
}
