using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductAttributeCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductAttributeResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Services.ProductAttributeService
{
    public class ProductAttributeService(IMediator mediator) : IProductAttributeService
    {
        private readonly IMediator _mediator = mediator;
        public async Task<TaskListResult<UpdateProductAttributeResult>> UpdateProductAttributes(BulkUpdateProductAttributeCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result;
        }
    }
}
