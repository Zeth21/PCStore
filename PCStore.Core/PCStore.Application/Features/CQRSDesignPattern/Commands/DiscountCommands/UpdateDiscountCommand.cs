using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.DiscountResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.DiscountCommands
{
    public class UpdateDiscountCommand : IRequest<TaskResult<UpdateDiscountResult>>
    {
        public int DiscountId { get; set; }
        public string? DiscountName { get; set; }
        public DateTime? DiscountEndDate { get; set; }
        public bool? DiscountIsPercentage { get; set; }
        public decimal? DiscountRate { get; set; }
        public string? Description { get; set; }
    }
}
