using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Queries.FollowedProductsQuery;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.FollowedProductResults;
using PCStore.Application.Features.Helpers.Validators.DiscountValidator;
using PCStore.Application.Features.Helpers.Validators.DiscountValidator.Commands;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.FollowedProductHandlers
{
    public class GetFollowedProductsHandler(IMapper mapper, ProjectDbContext context, IDiscountChecker checker) : IRequestHandler<GetFollowedProductsQuery,TaskListResult<GetFollowedProductsResult>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ProjectDbContext _context = context;

        public async Task<TaskListResult<GetFollowedProductsResult>> Handle(GetFollowedProductsQuery request, CancellationToken cancellationToken)
        {
            var checkUser = await _context.Users.AnyAsync(x => x.Id == request.UserId);
            if (!checkUser)
                return TaskListResult<GetFollowedProductsResult>.NotFound("User not found!");
            var followedProducts = await _context.Products
                .Include(p => p.FollowedProducts)
                .Where(p => p.FollowedProducts != null && p.FollowedProducts.Any(fp => fp.UserId == request.UserId))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            var discountCheckedList = await checker.CheckDiscount(_mapper.Map<List<DiscountValidatorCommand>>(followedProducts));
            var result = _mapper.Map<List<GetFollowedProductsResult>>(followedProducts);

            foreach(var fp in followedProducts) 
            {
                var dp = result.Find(x => x.ProductId == fp.ProductId);
                var id = fp.FollowedProducts!.FirstOrDefault()!.Id;
                if(id is not 0)
                    dp!.Id = id;
                var discProduct = discountCheckedList.Where(x => x.ProductId == fp.ProductId).SingleOrDefault();
                if (discProduct is not null)
                    _mapper.Map(discProduct, dp);
            }
            if (followedProducts.Count == 0 || followedProducts == null)
                return TaskListResult<GetFollowedProductsResult>.NotFound("No followings found!");
            return TaskListResult<GetFollowedProductsResult>.Success("All followings are found!", data:result);
        }
    }
}
