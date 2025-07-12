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
    public class RemoveShopCartItemHandler(ProjectDbContext context) : IRequestHandler<RemoveShopCartItemCommand, Result>
    {
        private readonly ProjectDbContext _context = context;

        public async Task<Result> Handle(RemoveShopCartItemCommand request, CancellationToken cancellationToken)
        {
            var checkUser = await _context.Users
                .AnyAsync(x => x.Id == request.UserId,cancellationToken);
            if (!checkUser)
                return Result.Fail("Invalid user!");
            var checkItem = await _context.ShoppingCartItems
                .FindAsync(request.Id,cancellationToken);
            if (checkItem is null || checkItem.UserId != request.UserId)
                return Result.NotFound("Product not found!");
            try 
            {
                _context.ShoppingCartItems.Remove(checkItem);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch(Exception ex) 
            {
                return Result.Fail("Something went wrong! " + ex.Message);
            }
            return Result.Success("Product removed successfully!");
        }
    }
}
