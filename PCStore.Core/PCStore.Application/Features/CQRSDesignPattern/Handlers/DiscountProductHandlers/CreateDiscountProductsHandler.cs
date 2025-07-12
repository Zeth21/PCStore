using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.DiscountProductCommand;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.DiscountProductResults;
using PCStore.Application.Features.Helpers.Validators.DiscountValidator;
using PCStore.Application.Features.Helpers.Validators.DiscountValidator.Commands;
using PCStore.Application.Services.DiscountProductService;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.DiscountProductHandlers
{
    public class CreateDiscountProductsHandler(ProjectDbContext context, IMapper mapper, IDiscountChecker checker) : IRequestHandler<CreateDiscountProductsCommand, TaskListResult<CreateDiscountProductResult>>
    {
        public async Task<TaskListResult<CreateDiscountProductResult>> Handle(CreateDiscountProductsCommand request, CancellationToken cancellationToken)
        {
            var discountExists = await context.Discounts
                .AnyAsync(x => x.DiscountId == request.DiscountId);
            if (!discountExists)
                return TaskListResult<CreateDiscountProductResult>.Fail("No discounts found!");
            var productsExists = await context.Products
                .AsNoTracking()
                .Where(x => request.ProductIds.Contains(x.ProductId))
                .ToListAsync(cancellationToken);
            if (productsExists.Count != request.ProductIds.Count)
                return TaskListResult<CreateDiscountProductResult>.Fail("One or more products are invalid!");
            var dpList = new List<DiscountProduct>();
            foreach (var id in request.ProductIds)
            {
                var newDp = new DiscountProduct { DiscountId = request.DiscountId, ProductId = id };
                dpList.Add(newDp);
            }
            try
            {
                await context.DiscountProducts.AddRangeAsync(dpList, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                var checkList = await checker.CheckDiscount(mapper.Map<List<DiscountValidatorCommand>>(productsExists));
                var result = mapper.Map<List<CreateDiscountProductResult>>(productsExists);
                var checkListDic = checkList.ToDictionary(x => x.ProductId);
                foreach(var item in result) 
                {
                    if(checkListDic.TryGetValue(item.ProductId, out var discount)) 
                    {
                        mapper.Map(discount, item);
                    }
                }
                foreach (var dp in dpList)
                {
                    var res = result.FirstOrDefault(x => x.ProductId == dp.ProductId);
                    res!.Id = dp.Id;
                }
                return TaskListResult<CreateDiscountProductResult>.Success("All products added to the discount successfully!", data: result);
            }
            catch (Exception ex)
            {
                return TaskListResult<CreateDiscountProductResult>.Fail("Something went wrong! " + ex.Message);
            }
        }
    }
}
