using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponResults;
using PCStore.Domain.Enum;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.CouponCommands
{
    public class UpdateCouponCommand : IRequest<TaskResult<UpdateCouponResult>>
    {
        public int CouponId { get; set; }
        public decimal? CouponValue { get; set; }
        public bool? CouponIsPercentage { get; set; } 
        public int? CouponMaxUsage { get; set; }
        public int? CouponMaxUsagePerUser { get; set; }
        public int? CouponMinOrderAmount { get; set; }
        public bool? CouponIsActive { get; set; }
        public string? Description { get; set; }
        public string? CouponCode { get; set; }
        public CouponTargetType? CouponTargetType { get; set; }
    }
}
