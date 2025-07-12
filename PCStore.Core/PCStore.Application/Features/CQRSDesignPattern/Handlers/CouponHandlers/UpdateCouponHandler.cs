using AutoMapper;
using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.UserResults;
using PCStore.Domain.Enum;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.CouponHandlers
{
    public class UpdateCouponHandler(ProjectDbContext context, IMapper mapper) : IRequestHandler<UpdateCouponCommand, TaskResult<UpdateCouponResult>>
    {
        public async Task<TaskResult<UpdateCouponResult>> Handle(UpdateCouponCommand request, CancellationToken cancellationToken)
        {
            var coupon = await context.Coupons
                .FindAsync(request.CouponId, cancellationToken);
            if (coupon is null)
                return TaskResult<UpdateCouponResult>.NotFound("Coupon not found!");
            CouponTargetType oldTarget = coupon.CouponTargetType;
            try
            {
                foreach (var prop in request.GetType().GetProperties())
                {
                    if (prop.Name == "Id" || prop.Name == "CouponId")
                        continue;
                    var value = prop.GetValue(request);
                    if (value is not null)
                    {
                        var couponProp = coupon.GetType().GetProperty(prop.Name);
                        if (couponProp is not null)
                        {
                            var targetType = Nullable.GetUnderlyingType(couponProp.PropertyType) ?? couponProp.PropertyType;
                            var convertedValue = Convert.ChangeType(value, targetType);
                            couponProp.SetValue(coupon, convertedValue);
                            continue;
                        }
                        return TaskResult<UpdateCouponResult>.Fail("Unknown property detected!");
                    }
                }
                await context.SaveChangesAsync();
                var result = mapper.Map<UpdateCouponResult>(coupon);
                if (request.CouponTargetType is not null)
                    result.OldTarget = oldTarget;
                return TaskResult<UpdateCouponResult>.Success("Coupon updated successfully!", data: result);
            }
            catch (Exception ex)
            {
                return TaskResult<UpdateCouponResult>.Fail("Something went wrong while saving the data! " + ex.Message);
            }
        }
    }
}
