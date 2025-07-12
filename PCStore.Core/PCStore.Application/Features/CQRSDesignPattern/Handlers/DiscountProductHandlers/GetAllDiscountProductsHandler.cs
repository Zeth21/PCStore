using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Queries.DiscountProductQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.DiscountProductResults;
using PCStore.Application.Features.Helpers.Validators.DiscountValidator;
using PCStore.Application.Features.Helpers.Validators.DiscountValidator.Commands;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.DiscountProductHandlers
{
    public class GetAllDiscountProductsHandler(ProjectDbContext context, IMapper mapper, IDiscountChecker checker) : IRequestHandler<GetAllDiscountProductsQuery, TaskListResult<GetAllDiscountProductsResult>>
    {
        public async Task<TaskListResult<GetAllDiscountProductsResult>> Handle(GetAllDiscountProductsQuery request, CancellationToken cancellationToken)
        {
            var productList = await context.Products
                .Include(x => x.DiscountProducts)
                .Where(p => p.DiscountProducts != null && p.DiscountProducts.Any(dp => dp.DiscountId == request.DiscountId))
                .ToListAsync(cancellationToken);
            if (productList.Count <= 0)
                return TaskListResult<GetAllDiscountProductsResult>.NotFound("No discount products found!");
            var checkerList = await checker.CheckDiscount(mapper.Map<List<DiscountValidatorCommand>>(productList));
            var result = mapper.Map<List<GetAllDiscountProductsResult>>(productList);
            foreach(var product in result) 
            {
                var id = productList.Where(x => x.DiscountProducts!.Any(x => x.ProductId == product.ProductId)).FirstOrDefault();
                product.Id = id!.DiscountProducts!.FirstOrDefault()!.Id;
                var discountCalc = checkerList.Where(x => x.ProductId == product.ProductId).SingleOrDefault();
                mapper.Map(discountCalc, product);
            }
            return TaskListResult<GetAllDiscountProductsResult>.Success("All products found successfully!",data:result);
        }
    }
}
