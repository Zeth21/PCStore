using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponProductCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponProductsHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Services.CouponProductService
{
    public class CouponProductService(Mediator mediator) : ICouponProductService
    {
        Task<TaskResult<ListCreateCouponProductResult>> ICouponProductService.CreateCouponProducts(ListCreateCouponProductCommand request, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        async Task<Result> ICouponProductService.RemoveCouponProducts(ListRemoveCouponProductsCommand request, CancellationToken cancellation)
        {
            var result = await mediator.Send(request, cancellation);
            return result;
        }
    }
}
