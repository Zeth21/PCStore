using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponBrandCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponBrandResults;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.CouponBrandHandlers
{
    public class CreateCouponBrandHandler(ProjectDbContext context, IMapper mapper) : IRequestHandler<CreateCouponBrandCommand, TaskResult<CreateCouponBrandResult>>
    {
        public async Task<TaskResult<CreateCouponBrandResult>> Handle(CreateCouponBrandCommand request, CancellationToken cancellationToken)
        {
            var checkCoupon = await context.Coupons
                .AnyAsync(x => x.CouponId == request.CouponId, cancellationToken);
            if (!checkCoupon)
                return TaskResult<CreateCouponBrandResult>.NotFound("Coupon not found!");
            var checkBrand = await context.Brands
                .SingleOrDefaultAsync(x => x.BrandId == request.BrandId, cancellationToken);
            if (checkBrand is null)
                return TaskResult<CreateCouponBrandResult>.NotFound("Brand not found!");
            try 
            {
                var newRecord = mapper.Map<CouponBrand>(request);
                await context.CouponBrands.AddAsync(newRecord,cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                var result = mapper.Map<CreateCouponBrandResult>(checkBrand);
                return TaskResult<CreateCouponBrandResult>.Success("Record created successfully!", data: result);
            }
            catch(Exception ex) 
            {
                return TaskResult<CreateCouponBrandResult>.Fail("Something went wrong! " + ex.Message);
            }
        }
    }
}
