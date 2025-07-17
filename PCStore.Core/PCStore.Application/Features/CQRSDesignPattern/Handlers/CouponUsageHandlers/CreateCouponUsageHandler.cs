using AutoMapper;
using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponUsageCommand;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.CouponUsageHandlers
{
    public class CreateCouponUsageHandler(ProjectDbContext context, IMapper mapper) : IRequestHandler<CreateCouponUsageCommand, Result>
    {
        public async Task<Result> Handle(CreateCouponUsageCommand request, CancellationToken cancellationToken)
        {
            var newRecord = mapper.Map<CouponUsage>(request);
            try 
            {
                await context.CouponUsages.AddAsync(newRecord,cancellationToken);
                var task = await context.SaveChangesAsync(cancellationToken);
                if (task <= 0)
                    return Result.Fail("Couldn't save the data!");
                return Result.Success("Coupon usage created successfully!");
            }
            catch(Exception ex) 
            {
                return Result.Fail("Something went wrong! " + ex.Message);
            }
        }
    }
}
