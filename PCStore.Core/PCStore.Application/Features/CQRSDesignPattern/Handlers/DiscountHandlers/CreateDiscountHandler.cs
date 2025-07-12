using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.DiscountCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.DiscountResults;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.DiscountHandlers
{
    public class CreateDiscountHandler(ProjectDbContext context, IMapper mapper) : IRequestHandler<CreateDiscountCommand, TaskResult<CreateDiscountResult>>
    {
        public async Task<TaskResult<CreateDiscountResult>> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var checkName = await context.Discounts
                .Where(x => x.DiscountName == request.DiscountName)
                .AnyAsync();
            if (checkName)
                return TaskResult<CreateDiscountResult>.Fail("Discount name already exists!");
            try 
            {
                var discount = mapper.Map<Discount>(request);
                await context.Discounts.AddAsync(discount, cancellationToken);
                var task = await context.SaveChangesAsync(cancellationToken);
                if (task <= 0)
                    return TaskResult<CreateDiscountResult>.Fail("Couldn't save the data!");
                var result = mapper.Map<CreateDiscountResult>(discount);
                return TaskResult<CreateDiscountResult>.Success("Discount created successfully! ", data : result);
            }
            catch(Exception ex) 
            {
                return TaskResult<CreateDiscountResult>.Fail("Something went wrong! " + ex.Message);
            }
        }
    }
}
