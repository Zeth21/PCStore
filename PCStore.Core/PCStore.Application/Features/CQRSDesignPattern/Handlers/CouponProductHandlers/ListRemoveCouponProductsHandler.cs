using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponProductCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.CouponProductHandlers
{
    class ListRemoveCouponProductsHandler(ProjectDbContext context) : IRequestHandler<ListRemoveCouponProductsCommand, Result>
    {
        public async Task<Result> Handle(ListRemoveCouponProductsCommand request, CancellationToken cancellationToken)
        {
            var productList = await context.CouponProducts
                .Where(x => x.CouponId == request.CouponId)
                .ToListAsync(cancellationToken);
            if (productList.Count <= 0)
                return Result.Fail("No records found!");
            try 
            {
                context.CouponProducts.RemoveRange(productList);
                await context.SaveChangesAsync(cancellationToken);
                return Result.Success("All products removed successfully!");
            }
            catch(Exception ex) 
            {
                return Result.Fail("Something went wrong while removing the data! " + ex.Message);
            }
        }
    }
}
