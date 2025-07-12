using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponResults;
using PCStore.Domain.Enum;
using System.Text.Json.Serialization;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.CouponCommands
{
    public class CreateCouponCommand : IRequest<TaskResult<CreateCouponResult>>
    {
        public decimal CouponValue { get; set; }
        public bool CouponIsPercentage { get; set; }
        public int CouponMaxUsage { get; set; }
        public int CouponMaxUsagePerUser { get; set; }
        public int CouponMinOrderAmount { get; set; }

        [JsonIgnore]
        public DateTime CouponStartTime { get; set; } = DateTime.Now;
        public DateTime CouponEndTime { get; set; } = DateTime.Now.AddDays(7);
        public bool CouponIsActive { get; set; }
        public required string Description { get; set; }
        public required string CouponCode { get; set; }
        public CouponTargetType CouponTargetType { get; set; }
    }
}
