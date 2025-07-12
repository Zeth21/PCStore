using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponCategoryCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.CouponCategoryHandlers
{
    public class RemoveCouponCategoryHandler(ProjectDbContext context) : IRequestHandler<RemoveCouponCategoryCommand, Result>
    {
        public async Task<Result> Handle(RemoveCouponCategoryCommand request, CancellationToken cancellationToken)
        {
            var checkCoupon = await context.Coupons
                .AsNoTracking()
                .AnyAsync(x => x.CouponId == request.CouponId,cancellationToken);
            if (!checkCoupon)
                return Result.NotFound("Coupon not found!");
            var couponCategory = await context.CouponCategories
                .SingleOrDefaultAsync(x => x.CouponId == request.CouponId, cancellationToken);
            if (couponCategory is null)
                return Result.NotFound("Record not found!");
            try 
            {
                context.CouponCategories.Remove(couponCategory);
                await context.SaveChangesAsync(cancellationToken);
                return Result.Success("Record removed successfully!");
            }
            catch(Exception ex) 
            {
                return Result.Fail("Something went wrong! " + ex.Message);
            }
        }
    }
}
