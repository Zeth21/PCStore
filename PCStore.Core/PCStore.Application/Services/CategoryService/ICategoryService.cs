using PCStore.Application.Features.CQRSDesignPattern.Commands.CategoryCommands;
using PCStore.Application.Features.CQRSDesignPattern.Handlers.CategoryHandlers;
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
    public interface ICategoryService
    {
        Task<TaskResult<CreateCategoryResult>> CreateCategory(CreateCategoryCommand request, CancellationToken cancellation);
        Task<TaskResult<GetAllCategoriesResult>> GetAllCategories(GetAllCategoriesQuery request, CancellationToken cancellation);
        Task<TaskResult<UpdateCategoryResult>> UpdateCategory(UpdateCategoryCommand request, CancellationToken cancellation);
    }
}
