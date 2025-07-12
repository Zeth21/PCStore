using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Commands.DiscountCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.DiscountQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.DiscountResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Services.DiscountService
{
    public class DiscountService(IMediator mediator) : IDiscountService
    {
        public async Task<Result> ActiveDiscount(ActiveDiscountCommand request, CancellationToken cancellation)
        {
            var result = await mediator.Send(request, cancellation);
            return result;
        }

        public async Task<TaskResult<CreateDiscountResult>> CreateDiscount(CreateDiscountCommand request, CancellationToken cancellation)
        {
            var result = await mediator.Send(request, cancellation);
            return result;
        }

        public async Task<Result> DeactiveDiscount(DeactiveDiscountCommand request, CancellationToken cancellation)
        {
            var result = await mediator.Send(request, cancellation);
            return result;
        }

        public async Task<TaskListResult<GetAllDiscountsResult>> GetAllDiscounts(GetAllDiscountsQuery request, CancellationToken cancellation)
        {
            var result = await mediator.Send(request, cancellation);
            return result;
        }

        public async Task<TaskResult<UpdateDiscountResult>> UpdateDiscount(UpdateDiscountCommand request, CancellationToken cancellation)
        {
            var result = await mediator.Send(request, cancellation);
            return result;
        }
    }
}
