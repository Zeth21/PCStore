using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CategoryCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CategoryResults;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.CategoryHandlers
{
    public class UpdateCategoryHandler(ProjectDbContext context, IMapper mapper) : IRequestHandler<UpdateCategoryCommand, TaskResult<UpdateCategoryResult>>
    {
        public async Task<TaskResult<UpdateCategoryResult>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            try 
            {
                var category = await context.Categories
                    .SingleOrDefaultAsync(x => x.CategoryId == request.CategoryId,cancellationToken);
                if (category is null)
                    return TaskResult<UpdateCategoryResult>.NotFound("No categories has found!");
                if (!string.IsNullOrEmpty(request.CategoryName)) 
                {
                    if (category.CategoryName == request.CategoryName)
                        return TaskResult<UpdateCategoryResult>.Fail("No changes detected!");
                    var nameExists = await context.Categories
                        .AnyAsync(x => x.CategoryName == request.CategoryName,cancellationToken);
                    if (nameExists)
                        return TaskResult<UpdateCategoryResult>.Fail("The name is already taken!");
                    category.CategoryName = request.CategoryName;
                }
                if(request.ParentCategoryId is not null) 
                {
                    if (request.ParentCategoryId == request.CategoryId)
                        return TaskResult<UpdateCategoryResult>.Fail("A category cannot be its own parent!");
                    var checkParent = await context.Categories
                        .AnyAsync(x => x.CategoryId == request.ParentCategoryId,cancellationToken);
                    if (!checkParent)
                        return TaskResult<UpdateCategoryResult>.Fail("Invalid parent category!");
                    category.ParentCategoryId = request.ParentCategoryId;
                }
                var affectedRows = await context.SaveChangesAsync(cancellationToken);
                if (affectedRows < 1)
                    return TaskResult<UpdateCategoryResult>.Fail("Couldn't save the data!");
                var result = mapper.Map<UpdateCategoryResult>(category);
                return TaskResult<UpdateCategoryResult>.Success("Category has updated successfully!", result);
            }
            catch (Exception ex)
            {
                return TaskResult<UpdateCategoryResult>.Fail("Something went wrong! " + ex.Message);
            }
        }
    }
}
