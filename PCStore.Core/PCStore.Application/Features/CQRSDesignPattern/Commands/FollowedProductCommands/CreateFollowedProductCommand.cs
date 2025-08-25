using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.FollowedProductResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.FollowedProductCommands
{
    public class CreateFollowedProductCommand : IRequest<TaskResult<CreateFollowedProductResult>>
    {
        public int ProductId { get; set; }
        [JsonIgnore]
        public string? UserId { get; set; } = "";
    }
}
