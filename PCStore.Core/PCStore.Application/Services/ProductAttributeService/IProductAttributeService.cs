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
    public interface IProductAttributeService
    {
        Task<TaskListResult<UpdateProductAttributeResult>> UpdateProductAttributes(BulkUpdateProductAttributeCommand request, CancellationToken cancellationToken);
    }
}
