using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.BrandCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.BrandResults;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.BrandHandlers
{
    public class CreateBrandHandler(ProjectDbContext context, IMapper mapper) : IRequestHandler<CreateBrandCommand, TaskResult<CreateBrandResult>>
    {
        public async Task<TaskResult<CreateBrandResult>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {

            var newBrand = mapper.Map<Brand>(request);
            try 
            {
                var brandNameExists = await context.Brands
                    .AnyAsync(x => x.BrandName == request.BrandName,cancellationToken);
                if (brandNameExists)
                    return TaskResult<CreateBrandResult>.Fail("Brand name is already taken!");
                await context.Brands.AddAsync(newBrand, cancellationToken);
                var task = await context.SaveChangesAsync(cancellationToken);
                if (task < 1)
                    return TaskResult<CreateBrandResult>.Fail("Couldn't save the data!");
                var result = mapper.Map<CreateBrandResult>(newBrand);
                return TaskResult<CreateBrandResult>.Success("Brand created successfully!", result);
            }
            catch(Exception ex) 
            {
                return TaskResult<CreateBrandResult>.Fail("Something went wrong! " + ex.Message);
            }
        }
    }
}
