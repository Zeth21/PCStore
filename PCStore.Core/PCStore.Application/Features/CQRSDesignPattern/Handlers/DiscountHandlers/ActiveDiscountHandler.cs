using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Commands.DiscountCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.DiscountHandlers
{
    public class ActiveDiscountHandler(ProjectDbContext context) : IRequestHandler<ActiveDiscountCommand, Result>
    {
        public async Task<Result> Handle(ActiveDiscountCommand request, CancellationToken cancellationToken)
        {
            var discount = await context.Discounts
                .FindAsync(request.DiscountId, cancellationToken);
            if (discount is null)
                return Result.NotFound("No discounts found!");
            if (discount.DiscountIsActive)
                return Result.Fail("Discount is already active!");
            discount.DiscountIsActive = true;
            discount.DiscountStartDate = DateTime.Now;
            discount.DiscountEndDate = DateTime.Now.AddDays(7);
            try
            {
                await context.SaveChangesAsync(cancellationToken);
                return Result.Success("Discount activated successfully!");
            }
            catch (Exception ex)
            {
                return Result.Fail("Something went wrong! " + ex.Message);
            }
        }
    }
}
