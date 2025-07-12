using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.ProductTypeAttributeCommands
{
    public class RemoveTypeAttributeCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
