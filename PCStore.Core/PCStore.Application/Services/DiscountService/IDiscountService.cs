using PCStore.Application.Features.CQRSDesignPattern.Commands.DiscountCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.DiscountQueries;
using PCStore.Application.Features.CQRSDesignPattern.Queries.DiscountUsageQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.DiscountResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.DiscountUsageResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Services.DiscountService
{
    public interface IDiscountService
    {
        Task<TaskResult<CreateDiscountResult>> CreateDiscount(CreateDiscountCommand request, CancellationToken cancellation);
        Task<Result> DeactiveDiscount(DeactiveDiscountCommand request, CancellationToken cancellation);
        Task<Result> ActiveDiscount(ActiveDiscountCommand request, CancellationToken cancellation);
        Task<TaskResult<UpdateDiscountResult>> UpdateDiscount(UpdateDiscountCommand request, CancellationToken cancellation);
        Task<TaskListResult<GetAllDiscountsResult>> GetAllDiscounts(GetAllDiscountsQuery request, CancellationToken cancellation);
        Task<TaskListResult<GetAllDiscountUsagesResult>> GetAllDiscountUsages(GetAllDiscountUsagesQuery request, CancellationToken cancellation);
    }
}
