using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductResults;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.ProductHandlers
{
    public class CreateProductHandler(IMapper mapper, ProjectDbContext context) : IRequestHandler<CreateProductCommand, TaskResult<CreateProductResult>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ProjectDbContext _context = context;

        public async Task<TaskResult<CreateProductResult>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var checkProductName = await _context.Products
                .Where(x => x.ProductName == request.ProductName)
                .FirstOrDefaultAsync(cancellationToken);
            if (checkProductName is not null)
                return TaskResult<CreateProductResult>.Fail(message: "Product name is invalid!");

            var checkType = await _context.ProductTypes
                .Where(x => x.Id == request.ProductTypeId)
                .FirstOrDefaultAsync(cancellationToken);
            if (checkType is null)
                return TaskResult<CreateProductResult>.Fail(message: "Type is invalid!");

            var checkBrand = await _context.Brands
                .Where(x => x.BrandId == request.ProductBrandId)
                .FirstOrDefaultAsync(cancellationToken);
            if (checkBrand is null)
                return TaskResult<CreateProductResult>.Fail(message: "Brand is invalid!");

            var checkCategory = await _context.Categories
                .Where(x => x.CategoryId == request.ProductCategoryId)
                .FirstOrDefaultAsync(cancellationToken);
            if (checkCategory is null)
                return TaskResult<CreateProductResult>.Fail(message: "Category is invalid!");

            try
            {
                var newProduct = _mapper.Map<Product>(request);
                await _context.Products.AddAsync(newProduct, cancellationToken);
                var task = await _context.SaveChangesAsync(cancellationToken);
                if (task == 0)
                    return TaskResult<CreateProductResult>.Fail(message: "Failed to save to database!");

                var result = await _context.Products
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .Where(x => x.ProductId == newProduct.ProductId)
                .Select(x => new CreateProductResult
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    BrandName = x.Brand!.BrandName,
                    CategoryName = x.Category!.CategoryName,
                    ProductPrice = x.ProductPrice,
                    ProductMainPhotoPath = x.ProductMainPhotoPath
                })
                .FirstOrDefaultAsync(cancellationToken);

                return TaskResult<CreateProductResult>.Success(message: "Product added successfully!", data: result);
            }
            catch (Exception ex)
            {
                return TaskResult<CreateProductResult>.Fail(message: "Process failed! " + ex.Message);
            }
        }
    }
}
