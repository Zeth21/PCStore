using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.ProductQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductResults;
using PCStore.Application.Services.ProductService.ProductServiceCommands;
using PCStore.Application.Services.ProductService.ProductServiceResults;

namespace PCStore.Application.Services.ProductService
{
    public interface IProductService
    {
        public Task<TaskResult<GetFacadeProductDetailsResult>> GetProductDetailsById(int id, CancellationToken cancellationToken);
        public Task<TaskListResult<GetAllProductsResult>> GetAllProducts(GetAllProductsQuery request, CancellationToken cancellationToken);
        public Task<Result> UpdateAvailableProduct(UpdateProductAvailabilityCommand request, CancellationToken cancellationToken);
        public Task<TaskResult<ServiceCreateProductResult>> CreateProduct(ServiceCreateProductCommand request, CancellationToken cancellationToken);
        public Task<TaskResult<UpdateProductResult>> UpdateProduct(UpdateProductCommand request, CancellationToken cancellationToken);
        public Task<TaskResult<UpdateProductTypeResult>> UpdateProductType(UpdateProductTypeCommand request, CancellationToken cancellationToken);
        public Task<TaskListResult<GetDiscountedProductsResult>> GetAllDiscountedProducts(GetDiscountedProductsQuery request, CancellationToken cancellationToken);
        public Task<TaskListResult<GetNewProductsResult>> GetNewProducts(GetNewProductsQuery request, CancellationToken cancellation);
        public Task<TaskListResult<GetBestSellingProductsResult>> GetBestSellingProducts(GetBestSellingProductsQuery request, CancellationToken cancellation);
    }
}
