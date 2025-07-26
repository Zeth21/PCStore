using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CategoryCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.CategoryQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CategoryResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Services.CategoryService
{
    public class CategoryService(IMediator mediator) : ICategoryService
    {
        public async Task<TaskResult<CreateCategoryResult>> CreateCategory(CreateCategoryCommand request, CancellationToken cancellation)
        {
            var result = await mediator.Send(request, cancellation);
            return result;
        }

        public async Task<TaskResult<GetAllCategoriesResult>> GetAllCategories(GetAllCategoriesQuery request, CancellationToken cancellation)
        {
            var result = await mediator.Send(request, cancellation);
            return result;
        }

        public async Task<TaskResult<UpdateCategoryResult>> UpdateCategory(UpdateCategoryCommand request, CancellationToken cancellation)
        {
            var result = await mediator.Send(request, cancellation);
            return result;
        }
    }
}
