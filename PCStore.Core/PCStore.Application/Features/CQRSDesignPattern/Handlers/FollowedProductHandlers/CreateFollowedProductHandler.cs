using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.FollowedProductCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.FollowedProductResults;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.FollowedProductHandlers
{
    public class CreateFollowedProductHandler(ProjectDbContext context, IMapper mapper) : IRequestHandler<CreateFollowedProductCommand, TaskResult<CreateFollowedProductResult>>
    {
        private readonly ProjectDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        public async Task<TaskResult<CreateFollowedProductResult>> Handle(CreateFollowedProductCommand request, CancellationToken cancellationToken)
        {
            var checkFollowing = await _context.FollowedProducts
                .Where(x => x.UserId == request.UserId && x.ProductId == request.ProductId)
                .FirstOrDefaultAsync(cancellationToken);
            if (checkFollowing is not null)
                return TaskResult<CreateFollowedProductResult>.Fail("You are already following that product!");
            var checkUser = await _context.Users
                .FindAsync(request.UserId, cancellationToken);
            if(checkUser is null)
                return TaskResult<CreateFollowedProductResult>.Fail("User doesn't exist!");
            var checkProduct = await _context.Products
                .FindAsync(request.ProductId,cancellationToken);
            if(checkProduct is null || !checkProduct.ProductIsAvailable)
                return TaskResult<CreateFollowedProductResult>.Fail("Product doesn't exist!");
            var following = new FollowedProduct
            {
                UserId = request.UserId,
                ProductId = request.ProductId
            };
            try 
            {
                await _context.AddAsync(following);
                await _context.SaveChangesAsync(cancellationToken);
                await _context.Entry(following).Reference(x => x.Product).LoadAsync(cancellationToken);
            }
            catch (Exception ex) 
            {
                return TaskResult<CreateFollowedProductResult>.Fail("Process has failed! " + ex.Message);
            }
            var result = _mapper.Map<CreateFollowedProductResult>(following);
            if (following.Product!.ProductStock <= 5)
                result.ProductStock = following.Product.ProductStock;
            return TaskResult<CreateFollowedProductResult>.Success("Product is added to the following list successfully! ", data:result);
        }
    }
}
