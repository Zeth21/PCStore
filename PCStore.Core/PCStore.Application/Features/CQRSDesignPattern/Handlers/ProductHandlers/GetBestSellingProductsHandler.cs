using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Queries.ProductQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductResults;
using PCStore.Application.Features.Helpers.Validators.DiscountValidator;
using PCStore.Application.Features.Helpers.Validators.DiscountValidator.Commands;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.ProductHandlers
{
    public class GetBestSellingProductsHandler : IRequestHandler<GetBestSellingProductsQuery, TaskListResult<GetBestSellingProductsResult>>
    {
        private readonly ProjectDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDiscountChecker _discountChecker;
        public GetBestSellingProductsHandler(ProjectDbContext context, IMapper mapper, IDiscountChecker discountChecker)
        {
            _context = context;
            _mapper = mapper;
            _discountChecker = discountChecker;
        }

        public async Task<TaskListResult<GetBestSellingProductsResult>> Handle(GetBestSellingProductsQuery request, CancellationToken cancellationToken)
        {
            try 
            {
                var productEntities = await _context.OrderProductLists
                    .Include(x => x.Product)
                    .GroupBy(x => x.ProductId)
                    .OrderByDescending(g => g.Sum(x => x.ProductQuantity))
                    .Skip(request.PageSize * (request.PageNumber -1))
                    .Take(request.PageSize)
                    .Select(g => g.First().Product)
                    .ToListAsync(cancellationToken);
                var products = _mapper.Map<List<GetBestSellingProductsResult>>(productEntities);
                var discountCheckerCommand = _mapper.Map<List<DiscountValidatorCommand>>(products);
                var discountList = await _discountChecker.CheckDiscount(discountCheckerCommand);
                var discountListDictionary = discountList.ToDictionary(x => x.ProductId);
                foreach (var product in products)
                {
                    if (discountListDictionary.TryGetValue(product.ProductId, out var discount))
                    {
                        _mapper.Map(discount, product);
                    }
                }
                return TaskListResult<GetBestSellingProductsResult>.Success("All products has found successfully!", products);
            }
            catch(Exception ex) 
            {
                return TaskListResult<GetBestSellingProductsResult>.Fail("Something went wrong! " + ex.Message);
            }

        }
    }
}
