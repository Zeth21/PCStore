using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.OrderProductListResults;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.OrderProductListCommands
{
    public class CreateOrderProductListCommand : IRequest<Result>
    {
        public int OrderId { get; set; }
        public required List<OrderProductListDTO> OrderProducts { get; set; }

    }

    public class OrderProductListDTO 
    {
        public int ProductId { get; set; }
        public byte ProductQuantity { get; set; }
        public decimal ProductTotalCost { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal? ProductOldPrice { get; set; }
        public decimal? ProductOldTotalCost { get; set; }
    }
}
