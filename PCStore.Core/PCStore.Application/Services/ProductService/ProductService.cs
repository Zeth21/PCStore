using AutoMapper;
using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.AttributeDefinitionQueries;
using PCStore.Application.Features.CQRSDesignPattern.Queries.BrandQueries;
using PCStore.Application.Features.CQRSDesignPattern.Queries.CommentQueries;
using PCStore.Application.Features.CQRSDesignPattern.Queries.ProductPhotoQueries;
using PCStore.Application.Features.CQRSDesignPattern.Queries.ProductQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductResults;
using PCStore.Application.Services.ProductService.ProductServiceCommands;
using PCStore.Application.Services.ProductService.ProductServiceResults;
using PCStore.Persistence.Context;

namespace PCStore.Application.Services.ProductService
{
    public class ProductService(IMediator mediator, ProjectDbContext context) : IProductService
    {
        private readonly IMediator _mediator = mediator;
        private readonly ProjectDbContext _context = context;
        public async Task<TaskResult<ServiceCreateProductResult>> CreateProduct(ServiceCreateProductCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var information = await _mediator.Send(request.Information, cancellationToken);
                if (information.Data is null)
                    return TaskResult<ServiceCreateProductResult>.Fail(message: information.Message ?? "Product Error!");
                request.Photos.PhotoProductId = information.Data.ProductId;
                request.Attributes.ProductId = information.Data.ProductId;
                var photos = await _mediator.Send(request.Photos, cancellationToken);
                var attributes = await _mediator.Send(request.Attributes, cancellationToken);

                if (!information.IsSucceeded || !attributes.IsSucceeded || !photos.IsSucceeded)
                {
                    var errorList = new List<string>();

                    if (!information.IsSucceeded)
                        errorList.Add(information.Message ?? "Information failed!");

                    if (!photos.IsSucceeded)
                        errorList.Add(photos.Message ?? "Photos failed!");

                    if (!attributes.IsSucceeded)
                        errorList.Add(attributes.Message ?? "Attributes failed!");

                    await transaction.RollbackAsync(cancellationToken);

                    return TaskResult<ServiceCreateProductResult>
                        .Fail(message: "One or more processes failed!", errors: errorList);

                }
                information.Data.ProductMainPhotoPath = photos.Data![0].PhotoPath;
                var result = new ServiceCreateProductResult
                {
                    Information = information.Data,
                    Attributes = attributes.Data,
                    Photos = photos.Data
                };
                await transaction.CommitAsync(cancellationToken);
                return TaskResult<ServiceCreateProductResult>.Success(message: "Product created successfully!", data: result);

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                return TaskResult<ServiceCreateProductResult>.Fail(message: "Process has failed! " + ex.Message);
            }
        }

        public async Task<TaskListResult<GetAllProductsResult>> GetAllProducts(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result;
        }

        public async Task<TaskResult<GetFacadeProductDetailsResult>> GetProductDetailsById(int id, CancellationToken cancellationToken)
        {
            var infoReq = new GetProductDetailsByIdQuery { ProductId = id };


            var information = await _mediator.Send(infoReq, cancellationToken);

            if (information.StatusCode == 404)
                return TaskResult<GetFacadeProductDetailsResult>.NotFound(message: "No products found!");
            else if (information.StatusCode == 400)
                return TaskResult<GetFacadeProductDetailsResult>.Fail(message: "Process failed", errors: information.Errors);

            var attrReq = new GetProductAttributesByIdQuery { ProductId = id };
            var photosReq = new GetPhotosByProductIdQuery { ProductId = id };
            var brandReq = new GetBrandNameByProductIdQuery { BrandId = id };
            var questionsReq = new GetQuestionsByProductIdQuery { ProductId = id };
            var commentsReq = new GetCommentsByProductIdQuery { CommentProductId = id };


            var attributes = await _mediator.Send(attrReq, cancellationToken);
            var photos = await _mediator.Send(photosReq, cancellationToken);
            var brand = await _mediator.Send(brandReq, cancellationToken);
            var questions = await _mediator.Send(questionsReq, cancellationToken);
            var comments = await _mediator.Send(commentsReq, cancellationToken);

            var result = new GetFacadeProductDetailsResult
            {
                Informations = information.Data,
                BrandName = brand.Data?.BrandName,
                Attributes = attributes.Data,
                Photos = photos.Data,
                Questions = questions.Data,
                Comments = comments.Data
            };

            return TaskResult<GetFacadeProductDetailsResult>.Success("Product found successfully!", data: result);
        }

        public async Task<Result> UpdateAvailableProduct(UpdateProductAvailabilityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var task = await _mediator.Send(request, cancellationToken);
                if (task.StatusCode == 404)
                    return Result.NotFound();
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Fail(message: ex.Message);
            }
        }

        public async Task<TaskResult<UpdateProductResult>> UpdateProduct(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result;
        }

        public async Task<TaskResult<UpdateProductTypeResult>> UpdateProductType(UpdateProductTypeCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result;
        }
    }
}
