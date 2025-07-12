using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Commands.DiscountProductCommand;
using PCStore.Application.Features.CQRSDesignPattern.Queries.DiscountProductQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.DiscountProductResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Services.DiscountProductService
{
    public class DiscountProductService(IMediator mediator) : IDiscountProductService
    {
        public async Task<TaskListResult<CreateDiscountProductResult>> CreateDiscountProducts(CreateDiscountProductsCommand request, CancellationToken cancellation)
        {
            var result = await mediator.Send(request, cancellation);
            return result;
        }

        public async Task<TaskListResult<GetAllDiscountProductsResult>> GetAllDiscountProducts(GetAllDiscountProductsQuery request, CancellationToken cancellation)
        {
            var result = await mediator.Send(request, cancellation);
            return result;
        }

        public async Task<Result> RemoveDiscountProducts(RemoveDiscountProductCommand request, CancellationToken cancellation)
        {
            var result = await mediator.Send(request,cancellation);
            return result;
        }

    }
}
