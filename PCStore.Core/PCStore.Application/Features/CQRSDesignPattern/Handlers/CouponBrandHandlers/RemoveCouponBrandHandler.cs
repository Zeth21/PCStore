using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponBrandCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.CouponBrandHandlers
{
    public class RemoveCouponBrandHandler(ProjectDbContext context) : IRequestHandler<RemoveCouponBrandCommand, Result>
    {
        public async Task<Result> Handle(RemoveCouponBrandCommand request, CancellationToken cancellationToken)
        {
            var couponExists = await context.Coupons
                .AsNoTracking()
                .AnyAsync(x => x.CouponId == request.CouponId, cancellationToken);
            if (!couponExists)
                return Result.NotFound("Coupon not found!");
            var couponBrand = await context.CouponBrands
                .SingleOrDefaultAsync(x => x.CouponId == request.CouponId, cancellationToken);
            if (couponBrand is null)
                return Result.NotFound("Record not found!");
            try 
            {
                context.CouponBrands.Remove(couponBrand);
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
