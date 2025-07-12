using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CreateShoppingCartItemsCommand;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.ShoppingCartItemHandlers
{
    public class CreateShopCartItemHandler(ProjectDbContext context, IMapper mapper) : IRequestHandler<CreateShopCartItemCommand,Result>
    {
        private readonly ProjectDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        public async Task<Result> Handle(CreateShopCartItemCommand request, CancellationToken cancellationToken)
        {
            var checkUser = await _context.Users.AnyAsync(x => x.Id == request.UserId,cancellationToken);
            if (!checkUser)
                return Result.NotFound("Invalid user!");
            var checkProduct = await _context.Products.AnyAsync(x => x.ProductId == request.ProductId,cancellationToken);
            if (!checkProduct)
                return Result.NotFound("Invalid product!");
            var checkItem = await _context.ShoppingCartItems
                .Where(x => x.UserId == request.UserId && x.ProductId == request.ProductId)
                .FirstOrDefaultAsync(cancellationToken);
            if (checkItem is not null) 
            {
                checkItem.ItemCount++;
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success("Product added to the cart successfully!");
            }
            try 
            {
                var newItem = _mapper.Map<ShoppingCartItem>(request);
                await _context.AddAsync(newItem);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch(Exception ex) 
            {
                return Result.Fail("Something went wrong! " + ex.Message);
            }
            return Result.Success("Product added to the cart successfully!");
        }
    }
}
