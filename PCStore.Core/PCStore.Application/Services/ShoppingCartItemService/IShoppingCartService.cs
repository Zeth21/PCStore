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
    public interface IShoppingCartService
    {
        public Task<Result> CreateShopCartItem(CreateShopCartItemCommand request, CancellationToken cancellation);
        public Task<Result> RemoveShopCartItem(RemoveShopCartItemCommand request, CancellationToken cancellation);
        public Task<TaskResult<BulkGetShopCartItemsResult>> GetShopCartItems(GetShopCartItemsQuery request, CancellationToken cancellation);
    }
}
