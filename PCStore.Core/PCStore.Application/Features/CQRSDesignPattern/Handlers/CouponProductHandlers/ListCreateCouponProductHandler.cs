using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponProductCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponProductsHandler;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.CouponProductHandlers
{
    public class ListCreateCouponProductHandler(ProjectDbContext context, IMapper mapper) : IRequestHandler<ListCreateCouponProductCommand, TaskListResult<ListCreateCouponProductResult>>
    {
        public async Task<TaskListResult<ListCreateCouponProductResult>> Handle(ListCreateCouponProductCommand request, CancellationToken cancellationToken)
        {
            var couponExists = await context.Coupons
                .AsNoTracking()
                .AnyAsync(x => x.CouponId == request.CouponId,cancellationToken);
            if (!couponExists)
                return TaskListResult<ListCreateCouponProductResult>.Fail("Coupon not found!");
            var productList = await context.Products
                .Where(x => request.ProductIds.Contains(x.ProductId) && x.ProductIsAvailable)
                .ToListAsync(cancellationToken);
            if (productList.Count != request.ProductIds.Count)
                return TaskListResult<ListCreateCouponProductResult>.Fail("One or more products are invalid!");
            var newList = new List<CouponProduct>();
            foreach(var product in productList) 
            {
                var newProduct = new CouponProduct { CouponId = request.CouponId, ProductId = product.ProductId, Product = product };
                newList.Add(newProduct);
            }
            try 
            {
                await context.CouponProducts.AddRangeAsync(newList, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                var result = mapper.Map<List<ListCreateCouponProductResult>>(newList);
                return TaskListResult<ListCreateCouponProductResult>.Success("Products added successfully!",data:result);
            }
            catch(Exception ex) 
            {
                return TaskListResult<ListCreateCouponProductResult>.Fail("Something went wrong while saving the data! " + ex.Message);
            }
        }
    }
}
