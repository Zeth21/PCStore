using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Persistence.Context;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.ProductHandlers
{
    public class UpdateProductAvailabilityHandler : IRequestHandler<UpdateProductAvailabilityCommand, Result>
    {
        private ProjectDbContext _projectDbContext;
        public UpdateProductAvailabilityHandler(ProjectDbContext projectDbContext)
        {
            _projectDbContext = projectDbContext;
        }

        async Task<Result> IRequestHandler<UpdateProductAvailabilityCommand, Result>.Handle(UpdateProductAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var checkProduct = await _projectDbContext.Products.FindAsync(request.ProductId, cancellationToken);
            if (checkProduct is null)
                return new Result { IsSucceeded = false, Message = "Product not found! " };
            checkProduct.ProductIsAvailable = request.IsAvailable;
            var result = await _projectDbContext.SaveChangesAsync(cancellationToken);
            if (result == 0)
                return new Result { IsSucceeded = false, Message = "Failed to update product!" };
            return new Result { IsSucceeded = true, Message = "Product is updated successfully!" };
        }
    }
}
