using MediatR;
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
    public class BrandService(IMediator mediator) : IBrandService
    {
        public async Task<TaskResult<CreateBrandResult>> CreateBrand(CreateBrandCommand request, CancellationToken cancellation)
        {
            var result = await mediator.Send(request, cancellation);
            return result;
        }

        public async Task<TaskListResult<GetAllBrandsResult>> GetAllBrands(GetAllBrandsQuery request, CancellationToken cancellation)
        {
            var result = await mediator.Send(request, cancellation);
            return result;
        }

        public async Task<TaskResult<UpdateBrandResult>> UpdateBrand(UpdateBrandCommand request, CancellationToken cancellation)
        {
            var result = await mediator.Send(request, cancellation);
            return result;
        }
    }
}
