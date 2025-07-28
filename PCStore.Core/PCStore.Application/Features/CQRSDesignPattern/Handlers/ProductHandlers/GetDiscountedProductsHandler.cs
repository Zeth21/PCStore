using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Queries.ProductQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductResults;
using PCStore.Application.Features.Helpers.Validators.DiscountValidator;
using PCStore.Application.Features.Helpers.Validators.DiscountValidator.Commands;
using PCStore.Application.Features.Helpers.Validators.DiscountValidator.Results;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.ProductHandlers
{
    public class GetDiscountedProductsHandler : IRequestHandler<GetDiscountedProductsQuery, TaskListResult<GetDiscountedProductsResult>>
    {
        private readonly ProjectDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDiscountChecker _discountChecker;
        public GetDiscountedProductsHandler(ProjectDbContext context, IMapper mapper, IDiscountChecker discountChecker)
        {
            _context = context;
            _mapper = mapper;
            _discountChecker = discountChecker;
        }

        public async Task<TaskListResult<GetDiscountedProductsResult>> Handle(GetDiscountedProductsQuery request, CancellationToken cancellationToken)
        {
            try 
            {
                var products = await _context.Products
                        .Include(x => x.DiscountProducts)
                        .Where(x => x.DiscountProducts != null && x.DiscountProducts.Any(x => x.Discount!.DiscountIsActive))
                        .AsNoTracking()
                        .Skip(request.PageSize * (request.PageNumber - 1))
                        .Take(request.PageSize)
                        .ProjectTo<GetDiscountedProductsResult>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);
                var discountRequest = _mapper.Map<List<DiscountValidatorCommand>>(products);
                var discountedList = await _discountChecker.CheckDiscount(discountRequest);
                var discountDictionary = discountedList.ToDictionary(x => x.ProductId);
                foreach (var product in products)
                {
                    if (discountDictionary.TryGetValue(product.ProductId, out var discount))
                    {
                        _mapper.Map(discount, product);
                    }
                }
                return TaskListResult<GetDiscountedProductsResult>.Success("All products has found successfully!", products);
            }
            catch(Exception ex) 
            {
                return TaskListResult<GetDiscountedProductsResult>.Fail("Something went wrong! " + ex.Message);
            }
        }
    }
}
