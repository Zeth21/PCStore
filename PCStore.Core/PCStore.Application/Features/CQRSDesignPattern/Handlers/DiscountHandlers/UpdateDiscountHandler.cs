using AutoMapper;
using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Commands.DiscountCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.DiscountResults;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.DiscountHandlers
{
    public class UpdateDiscountHandler(ProjectDbContext context, IMapper mapper) : IRequestHandler<UpdateDiscountCommand, TaskResult<UpdateDiscountResult>>
    {
        public async Task<TaskResult<UpdateDiscountResult>> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            var discount = await context.Discounts
                .FindAsync(request.DiscountId, cancellationToken);
            if (discount is null)
                return TaskResult<UpdateDiscountResult>.Fail("No discounts found!");
            var updateProps = request.GetType().GetProperties()
                .Where(x => x.GetValue(request) != null && x.Name != nameof(request.DiscountId) && x.CanWrite)
                .ToList();
            foreach (var prop in updateProps)
            {
                var value = prop.GetValue(request);
                var name = prop.Name;
                var discountProp = discount.GetType().GetProperty(name);
                if (discountProp != null && discountProp.CanWrite)
                    discountProp.SetValue(discount, value);
            }
            try
            {
                await context.SaveChangesAsync(cancellationToken);
                var result = mapper.Map<UpdateDiscountResult>(discount);
                return TaskResult<UpdateDiscountResult>.Success("Discount updated successfully! ", data: result);
            }
            catch (Exception ex)
            {
                return TaskResult<UpdateDiscountResult>.Fail("Something went wrong! " + ex.Message);
            }
        }
    }
}
