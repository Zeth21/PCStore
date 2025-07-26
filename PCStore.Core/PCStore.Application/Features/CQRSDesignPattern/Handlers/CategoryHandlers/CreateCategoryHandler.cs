using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CategoryCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CategoryResults;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.CategoryHandlers
{
    public class CreateCategoryHandler(ProjectDbContext context, IMapper mapper) : IRequestHandler<CreateCategoryCommand, TaskResult<CreateCategoryResult>>
    {
        public async Task<TaskResult<CreateCategoryResult>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            try 
            {
                var nameExists = await context.Categories
                .AnyAsync(x => x.CategoryName == request.CategoryName,cancellationToken);
                if (nameExists)
                    return TaskResult<CreateCategoryResult>.Fail("The name is already taken!");
                if (request.ParentCategoryId is not null)
                {
                    var checkParentId = await context.Categories
                        .AnyAsync(x => x.CategoryId == request.ParentCategoryId,cancellationToken);
                    if (!checkParentId)
                        return TaskResult<CreateCategoryResult>.Fail("Invalid parent category!");
                }
                var newCategory = mapper.Map<Category>(request);
                await context.Categories.AddAsync(newCategory, cancellationToken);
                var affectedRows = await context.SaveChangesAsync(cancellationToken);
                if (affectedRows < 1)
                    return TaskResult<CreateCategoryResult>.Fail("Couldn't save data to the database!");
                var result = mapper.Map<CreateCategoryResult>(newCategory);
                return TaskResult<CreateCategoryResult>.Success("Category has created successfully!", result);
            }
            catch(Exception ex) 
            {
                return TaskResult<CreateCategoryResult>.Fail("Something went wrong! " + ex.Message);
            }

        }
    }
}
