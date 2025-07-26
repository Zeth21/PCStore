using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.BrandCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.BrandResults;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.BrandHandlers
{
    public class UpdateBrandHandler(ProjectDbContext context, IMapper mapper) : IRequestHandler<UpdateBrandCommand, TaskResult<UpdateBrandResult>>
    {
        public async Task<TaskResult<UpdateBrandResult>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            try 
            {
                var brandNameExists = await context.Brands
                    .AnyAsync(x => x.BrandName == request.BrandName && x.BrandId != request.BrandId,cancellationToken);
                if (brandNameExists)
                    return TaskResult<UpdateBrandResult>.Fail("The name is already taken!");
                var brand = await context.Brands.FindAsync(new object[] { request.BrandId }, cancellationToken);
                if (brand is null)
                    return TaskResult<UpdateBrandResult>.NotFound("Brand not found!");
                if (brand.BrandName == request.BrandName)
                    return TaskResult<UpdateBrandResult>.Fail("No changes detected!");
                brand.BrandName = request.BrandName;
                var affectedRows = await context.SaveChangesAsync(cancellationToken);
                if (affectedRows < 1)
                    return TaskResult<UpdateBrandResult>.Fail("Couldn't save the data!");
                var result = mapper.Map<UpdateBrandResult>(brand);
                return TaskResult<UpdateBrandResult>.Success("Brand updated successfully!", result);
            }
            catch(Exception ex) 
            {
                return TaskResult<UpdateBrandResult>.Fail("Something went wrong! " + ex.Message);
            }
        }
    }
}
