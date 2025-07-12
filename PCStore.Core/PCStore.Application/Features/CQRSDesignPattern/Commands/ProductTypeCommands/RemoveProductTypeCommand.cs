using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.ProductTypeCommands
{
    public class RemoveProductTypeCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
