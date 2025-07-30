using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.CreateShoppingCartItemsCommand
{
    public class CreateShopCartItemCommand : IRequest<Result>
    {
        [JsonIgnore]
        public string? UserId { get; set; } = string.Empty;
        public int ProductId { get; set; }
    }
}
