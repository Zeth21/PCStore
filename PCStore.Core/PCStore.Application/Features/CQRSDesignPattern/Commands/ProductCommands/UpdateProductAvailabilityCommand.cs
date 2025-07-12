using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.ProductCommands
{
    public class UpdateProductAvailabilityCommand : IRequest<Result>
    {
        public int ProductId { get; set; }
        public bool IsAvailable { get; set; }
        public UpdateProductAvailabilityCommand(int productId, bool isAvailable)
        {
            ProductId = productId;
            IsAvailable = isAvailable;
        }
    }

}
