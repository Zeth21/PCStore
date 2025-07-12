using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponProductTypeCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.CouponProductTypeHandlers
{
    public class RemoveCouponProductTypeHandler(ProjectDbContext context) : IRequestHandler<RemoveCouponProductTypeCommand, Result>
    {
        public async Task<Result> Handle(RemoveCouponProductTypeCommand request, CancellationToken cancellationToken)
        {
            var checkCoupon = await context.Coupons
                .AnyAsync(x => x.CouponId == request.CouponId,cancellationToken);
            if (!checkCoupon)
                return Result.NotFound("Coupon not found!");
            var record = await context.CouponProductTypes
                .SingleOrDefaultAsync(x => x.CouponId == request.CouponId, cancellationToken);
            if (record is null)
                return Result.NotFound("Record not found!");
            try 
            {
                context.CouponProductTypes.Remove(record);
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
