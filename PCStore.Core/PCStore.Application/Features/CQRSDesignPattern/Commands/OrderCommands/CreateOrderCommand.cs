using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.OrderResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.OrderCommands
{
    public class CreateOrderCommand : IRequest<TaskResult<CreateOrderResult>>
    {
        public required string UserId { get; set; }
        public required int OrderAddressId { get; set; }
        public decimal OrderTotalCost { get; set; }
        public int? CouponId { get; set; }

        [JsonIgnore]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [JsonIgnore]
        public bool OrderIsActive { get; set; } = true;


    }
}
