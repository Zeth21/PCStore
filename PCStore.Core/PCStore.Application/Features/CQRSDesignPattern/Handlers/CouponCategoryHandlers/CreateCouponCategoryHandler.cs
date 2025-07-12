using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponCategoryCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponCategoryResults;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.CouponCategoryHandlers
{
    public class CreateCouponCategoryHandler(ProjectDbContext context, IMapper mapper) : IRequestHandler<CreateCouponCategoryCommand, TaskResult<CreateCouponCategoryResult>>
    {

        public async Task<TaskResult<CreateCouponCategoryResult>> Handle(CreateCouponCategoryCommand request, CancellationToken cancellationToken)
        {
            var checkCoupon = await context.Coupons
                .AsNoTracking()
                .AnyAsync(x => x.CouponId == request.CouponId, cancellationToken);
            if (!checkCoupon)
                return TaskResult<CreateCouponCategoryResult>.NotFound("Coupon not found!");
            var checkCategory = await context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CategoryId == request.CategoryId);
            if (checkCategory is null)
                return TaskResult<CreateCouponCategoryResult>.NotFound("Category not found!");
            try 
            {
                var newRecord = mapper.Map<CouponCategory>(request);
                await context.CouponCategories.AddAsync(newRecord,cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                var result = new CreateCouponCategoryResult { CategoryId = newRecord.Id,CategoryName = checkCategory.CategoryName!};
                return TaskResult<CreateCouponCategoryResult>.Success("Record created successfully!", data: result);
            }
            catch(Exception ex) 
            {
                return TaskResult<CreateCouponCategoryResult>.Fail("Something went wrong! " + ex.Message);
            }
        }
    }
}
