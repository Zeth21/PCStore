using AutoMapper;
using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Commands.DiscountUsageCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.Helpers.Validators.DiscountValidator;
using PCStore.Application.Features.Helpers.Validators.DiscountValidator.Commands;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.DiscountUsageHandlers
{
    public class CreateDiscountUsageHandler(ProjectDbContext context, IMapper mapper, IDiscountUsageCalculator calculator) : IRequestHandler<CreateDiscountUsageCommand, Result>
    {
        public async Task<Result> Handle(CreateDiscountUsageCommand request, CancellationToken cancellationToken)
        {
            var discUsageCommand = new DiscountUsageCalculatorCommand { UserId = request.UserId };
            var discountUsages = await calculator.CalculateDiscountUsage(discUsageCommand);
            foreach(var usage in discountUsages) 
            {
                usage.OrderId = request.OrderId;
            };
            if (discountUsages.Count <= 0)
                return Result.Success("No discounts found!");
            try 
            {
                var newRecords = mapper.Map<List<DiscountUsage>>(discountUsages);
                await context.DiscountUsages.AddRangeAsync(newRecords,cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                var ids = newRecords.Select(x => x.Id).ToList();
                return Result.Success("All discount usages created successfully!");
            }
            catch(Exception ex) 
            {
                return Result.Fail("Something went wrong! " + ex.Message);
            }
        }
    }
}
