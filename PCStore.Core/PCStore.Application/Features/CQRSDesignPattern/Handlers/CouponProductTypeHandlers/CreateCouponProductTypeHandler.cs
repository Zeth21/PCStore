using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponProductTypeCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponProductTypeResults;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.CouponProductTypeHandlers
{
    public class CreateCouponProductTypeHandler(ProjectDbContext context) : IRequestHandler<CreateCouponProductTypeCommand, TaskResult<CreateCouponProductTypeResult>>
    {
        public async Task<TaskResult<CreateCouponProductTypeResult>> Handle(CreateCouponProductTypeCommand request, CancellationToken cancellationToken)
        {
            var couponExists = await context.Coupons
                .AnyAsync(x => x.CouponId == request.CouponId, cancellationToken);
            if (!couponExists)
                return TaskResult<CreateCouponProductTypeResult>.NotFound("Coupon not found!");
            var checkType = await context.ProductTypes
                .FindAsync(request.TypeId,cancellationToken);
            if (checkType is null)
                return TaskResult<CreateCouponProductTypeResult>.Fail("Type is invalid!");
            try 
            {
                var newRecord = new CouponProductType { CouponId = request.CouponId, ProductTypeId = request.TypeId};
                await context.CouponProductTypes.AddAsync(newRecord, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                var result = new CreateCouponProductTypeResult { Id = newRecord.Id, Name = checkType.Name};
                return TaskResult<CreateCouponProductTypeResult>.Success("Record created successfully!",data:result);
            }
            catch (Exception ex) 
            {
                return TaskResult<CreateCouponProductTypeResult>.Fail("Something went wrong! " + ex.Message);
            }
        }
    }
}
