using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Queries.ProductQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductResults;
using PCStore.Application.Features.Helpers.Validators.DiscountValidator;
using PCStore.Application.Features.Helpers.Validators.DiscountValidator.Commands;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;
using System.Linq.Expressions;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.ProductHandlers
{
    public class GetAllProductsHandler(ProjectDbContext projectDbContext, IMapper mapper, IDiscountChecker discountChecker) : IRequestHandler<GetAllProductsQuery, TaskListResult<GetAllProductsResult>>
    {
        private readonly ProjectDbContext _projectDbContext = projectDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<TaskListResult<GetAllProductsResult>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var photoPath = await _projectDbContext.ProductPhotos.FirstAsync();
            var query = _projectDbContext.Products.Include(x => x.Brand).Include(x => x.Category).Include(x => x.ProductType).AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Name))
                query = query.Where(p => p.ProductName.StartsWith(request.Name));

            if (!string.IsNullOrWhiteSpace(request.CategoryName))
            {
                var checkCategory = await _projectDbContext.Categories.Where(x => x.CategoryName == request.CategoryName).Select(x => (int?)x.CategoryId).FirstOrDefaultAsync(cancellationToken);
                if (checkCategory.HasValue)
                {
                    query = query.Where(x => x.ProductCategoryId == checkCategory);
                }
            }
            if (request.MinPrice.HasValue)
                query = query.Where(p => p.ProductPrice >= request.MinPrice.Value);

            if (request.MaxPrice.HasValue)
                query = query.Where(p => p.ProductPrice <= request.MaxPrice.Value);
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                var parameter = Expression.Parameter(typeof(Product), "x");

                var propertyAccess = Expression.Property(parameter, request.SortBy);

                var converted = Expression.Convert(propertyAccess, typeof(object));
                var orderByExpression = Expression.Lambda<Func<Product, object>>(converted, parameter);


                bool orderByDescending = request.OrderByDesc ?? false;

                if (orderByDescending)
                {
                    query = query.OrderByDescending(orderByExpression);
                }
                else
                {
                    query = query.OrderBy(orderByExpression);
                }
            }

            var products = await query
                .Where(x => x.ProductIsAvailable == true && x.ProductStock > 0)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);
            if (products.Count <= 0)
                return TaskListResult<GetAllProductsResult>.NotFound("No products has found!");
            var checkList = await discountChecker.CheckDiscount(_mapper.Map<List<DiscountValidatorCommand>>(products));
            var result = _mapper.Map<List<GetAllProductsResult>>(products);
            var discountDict = checkList.ToDictionary(x => x.ProductId);
            foreach (var item in result)
            {
                if (discountDict.TryGetValue(item.ProductId, out var discount))
                {
                    _mapper.Map(discount, item); 
                }
            }
            return TaskListResult<GetAllProductsResult>.Success("All products has found successfully!",data:result);
        }
    }
}
