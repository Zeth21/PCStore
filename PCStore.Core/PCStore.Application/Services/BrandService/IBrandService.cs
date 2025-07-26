using PCStore.Application.Features.CQRSDesignPattern.Commands.BrandCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.BrandQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.BrandResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Services.BrandService
{
    public interface IBrandService
    {
        Task<TaskResult<CreateBrandResult>> CreateBrand(CreateBrandCommand request, CancellationToken cancellation);
        Task<TaskResult<UpdateBrandResult>> UpdateBrand(UpdateBrandCommand request, CancellationToken cancellation);
        Task<TaskListResult<GetAllBrandsResult>> GetAllBrands(GetAllBrandsQuery request, CancellationToken cancellation);
    }
}
