using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Queries.ProductQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductResults;
using PCStore.Application.Features.Helpers.Validators.DiscountValidator;
using PCStore.Application.Features.Helpers.Validators.DiscountValidator.Commands;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.ProductHandlers
{
    public class GetProductDetailsByIdHandler(IMapper mapper, ProjectDbContext context, IDiscountChecker checker) : IRequestHandler<GetProductDetailsByIdQuery, TaskResult<GetProductInformationsResult>>
    {
        public async Task<TaskResult<GetProductInformationsResult>> Handle(GetProductDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var checkProduct = await context.Products
                    .Where(x => x.ProductIsAvailable && x.ProductId == request.ProductId)
                    .SingleOrDefaultAsync(cancellationToken);
                if (checkProduct is null)
                    return TaskResult<GetProductInformationsResult>.NotFound("Product not found!");
                var productList = new List<Product> { checkProduct }; 
                var discountCheck = await checker.CheckDiscount(mapper.Map<List<DiscountValidatorCommand>>(productList));
                var result = mapper.Map<GetProductInformationsResult>(checkProduct);
                mapper.Map(discountCheck[0], result);
                return TaskResult<GetProductInformationsResult>.Success("Informations succeeded!", data:result);

            }
            catch (Exception ex)
            {
                return TaskResult<GetProductInformationsResult>.Fail("Process failed!", errors: [ex.Message]);
            }
        }

    }
}
