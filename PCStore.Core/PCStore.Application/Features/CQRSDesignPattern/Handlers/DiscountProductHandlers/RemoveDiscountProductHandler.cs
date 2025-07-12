using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.DiscountProductCommand;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.DiscountProductHandlers
{
    public class RemoveDiscountProductHandler(ProjectDbContext context) : IRequestHandler<RemoveDiscountProductCommand, Result>
    {
        public async Task<Result> Handle(RemoveDiscountProductCommand request, CancellationToken cancellationToken)
        {
            var discountExists = await context.Discounts
                .AnyAsync(x => x.DiscountId == request.DiscountId);
            if (!discountExists)
                return Result.Fail("Discount not found!");
            var removeList = await context.DiscountProducts
                .Where(x => request.ProductIds.Contains(x.ProductId) && x.DiscountId == request.DiscountId)
                .ToListAsync(cancellationToken);
            if (removeList.Count != request.ProductIds.Count)
                return Result.NotFound("No records found!");
            try 
            {
                context.DiscountProducts.RemoveRange(removeList);
                await context.SaveChangesAsync(cancellationToken);
                return Result.Success("All products removed from discount successfully!");
            }
            catch(Exception ex) 
            {
                return Result.Fail("Something went wrong! " + ex.Message);
            }
        }
    }
}
