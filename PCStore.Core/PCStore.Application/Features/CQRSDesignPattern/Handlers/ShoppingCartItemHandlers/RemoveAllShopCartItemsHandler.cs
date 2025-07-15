using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ShoppingCartItemsCommand;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.ShoppingCartItemHandlers
{
    public class RemoveAllShopCartItemsHandler(ProjectDbContext context) : IRequestHandler<RemoveAllShopCartItemsCommand, Result>
    {
        public async Task<Result> Handle(RemoveAllShopCartItemsCommand request, CancellationToken cancellationToken)
        {
            var shopCartItems = await context.ShoppingCartItems
                .Where(x => x.UserId == request.UserId)
                .ToListAsync(cancellationToken);
            if (shopCartItems.Count <= 0)
                return Result.NotFound("No items has found!");
            try 
            {
                context.ShoppingCartItems.RemoveRange(shopCartItems);
                var result = await context.SaveChangesAsync(cancellationToken);
                if (result <= 0)
                    return Result.Fail("Something went wrong!");
                return Result.Success("All items removed successfully!");
            }
            catch(Exception ex) 
            {
                return Result.Fail("Something went wrong! " + ex.Message);
            }
        }
    }
}
