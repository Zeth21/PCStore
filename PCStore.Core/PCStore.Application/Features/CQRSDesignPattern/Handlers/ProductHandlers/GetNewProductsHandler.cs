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

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.ProductHandlers
{
    public class GetNewProductsHandler : IRequestHandler<GetNewProductsQuery, TaskListResult<GetNewProductsResult>>
    {
        private readonly ProjectDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDiscountChecker _discountChecker;
        public GetNewProductsHandler(ProjectDbContext context, IMapper mapper, IDiscountChecker discountChecker)
        {
            _context = context;
            _mapper = mapper;
            _discountChecker = discountChecker;
        }

        public async Task<TaskListResult<GetNewProductsResult>> Handle(GetNewProductsQuery request, CancellationToken cancellationToken)
        {
            try 
            {
                var twoWeeksAgo = DateTime.Now.AddDays(-14);
                var products = await _context.Products
                    .Where(p => p.CreatedAt >= twoWeeksAgo)
                    .AsNoTracking()
                    .Skip(request.PageSize * (request.PageNumber -1))
                    .Take(request.PageSize)
                    .ProjectTo<GetNewProductsResult>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
                var discountCheckerCommand = _mapper.Map<List<DiscountValidatorCommand>>(products);
                var discountedList = await _discountChecker.CheckDiscount(discountCheckerCommand);
                var discountDictionary = discountedList.ToDictionary(x => x.ProductId);
                foreach (var product in products)
                {
                    if (discountDictionary.TryGetValue(product.ProductId, out var discount))
                    {
                        _mapper.Map(discount, product);
                    }
                }
                return TaskListResult<GetNewProductsResult>.Success("All products has found successfully!", products);
            }
            catch(Exception ex) 
            {
                return TaskListResult<GetNewProductsResult>.Fail("Something went wrong! " + ex.Message);
            }
        }
    }
}
