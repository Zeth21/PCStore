using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CreateShoppingCartItemsCommand;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ShoppingCartItemsCommand;
using PCStore.Application.Features.CQRSDesignPattern.Queries.ShoppingCartItemsQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.ShoppingCartItemResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Services.ShoppingCartItemService
{
    public class ShoppingCartService(IMediator mediator) : IShoppingCartService
    {
        private readonly IMediator _mediator = mediator;
        public async Task<Result> CreateShopCartItem(CreateShopCartItemCommand request, CancellationToken cancellation)
        {
            var result = await _mediator.Send(request, cancellation);
            return result;
        }

        public async Task<TaskResult<BulkGetShopCartItemsResult>> GetShopCartItems(GetShopCartItemsQuery request, CancellationToken cancellation)
        {
            var result = await _mediator.Send(request, cancellation);
            return result;
        }

        public async Task<Result> RemoveShopCartItem(RemoveShopCartItemCommand request, CancellationToken cancellation)
        {
            var result = await _mediator.Send(request, cancellation);
            return result;
        }
    }
}
