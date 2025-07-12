using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductAttributeResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.ProductAttributeCommands
{
    public class BulkUpdateProductAttributeCommand : IRequest<TaskListResult<UpdateProductAttributeResult>>
    {
        public int ProductId { get; set; }
        public required List<UpdateProductAttributeCommand> Attributes { get; set; }
    }
}
