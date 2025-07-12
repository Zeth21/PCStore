using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductResults;
using PCStore.Persistence.Context;


namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.ProductHandlers
{
    public class UpdateProductHandler(IMapper mapper, ProjectDbContext context) : IRequestHandler<UpdateProductCommand, TaskResult<UpdateProductResult>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ProjectDbContext _context = context;
        public async Task<TaskResult<UpdateProductResult>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var checkProduct = await _context.Products
                .Where(x => x.ProductId == request.ProductId)
                .FirstOrDefaultAsync(cancellationToken);
            if (checkProduct is null)
                return TaskResult<UpdateProductResult>.NotFound("Product not found!");
            if(request.ProductName is not null) 
            {
                var checkName = await _context.Products
                    .Where(x => x.ProductName == request.ProductName && x.ProductId != request.ProductId)
                    .AnyAsync(cancellationToken);
                if (checkName is true)
                    return TaskResult<UpdateProductResult>.Fail("The product name has already given!");
            }
            if(request.ProductCategoryId is not null) 
            {
                var checkCategory = await _context.Categories
                    .Where(x => x.CategoryId == request.ProductCategoryId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (checkCategory is null)
                    return TaskResult<UpdateProductResult>.Fail("Category not found!");
            }
            if (request.ProductBrandId is not null)
            {
                var checkBrand = await _context.Brands
                    .Where(x => x.BrandId == request.ProductBrandId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (checkBrand is null)
                    return TaskResult<UpdateProductResult>.Fail("Brand not found!");
            }
            try 
            {

                _mapper.Map(request, checkProduct);
                _context.Update(checkProduct);
                var task = await _context.SaveChangesAsync(cancellationToken);
                if (task <= 0)
                    return TaskResult<UpdateProductResult>.Fail("Something went wrong while saving the data to the database!");
                var product = await _context.Products
                    .Include(x => x.Brand)
                    .Include(x => x.Category)
                    .Where(x => x.ProductId == checkProduct.ProductId)
                    .FirstOrDefaultAsync(cancellationToken);
                var result = _mapper.Map<UpdateProductResult>(product);
                return TaskResult<UpdateProductResult>.Success("Product updated successfully!",data : result);
            }
            catch(Exception ex) 
            {
                return TaskResult<UpdateProductResult>.Fail("Something went wrong! " + ex.Message);
            }
        }
    }
}
